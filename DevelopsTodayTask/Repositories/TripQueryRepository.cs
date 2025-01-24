using DevelopsTodayTask.DbContext;
using DevelopsTodayTask.Models;
using DevelopsTodayTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevelopsTodayTask.Repositories
{
	public class TripQueryRepository : ITripQueryRepository
	{
		private readonly TripDbContext _context;

		public TripQueryRepository (TripDbContext context)
		{
			_context = context;
		}

		public async Task<(int? PULocationID, double? AverageTip)> GetPuLocationIdWithHighestAverageTipAsync ()
		{
			var result = await _context.TripData
				.GroupBy(t => t.PULocationID)
				.Select(group => new
				{
					PULocationID = group.Key,
					AverageTip = group.Average(t => t.TipAmount)
				})
				.OrderByDescending(x => x.AverageTip)
				.FirstOrDefaultAsync();

			return result != null ? (result.PULocationID, result.AverageTip) : (null, null);
		}

		public async Task<List<TripData>> GetLongestTripsAsync ()
		{
			var longestTrips = await _context.TripData
				.OrderByDescending(t => t.TripDistance)
				.Take(100)
				.ToListAsync();

			return longestTrips;
		}

		public async Task<List<TripData>> GetLongestTripsByDurationAsync ()
		{
			var longestDurationTrips = await _context.TripData
				.FromSqlRaw(@"
		             SELECT * 
			            FROM Trips 
			            ORDER BY DATEDIFF(SECOND, TpepPickupDatetime, TpepDropoffDatetime) DESC
			            OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY")
				.ToListAsync();

			return longestDurationTrips;
		}

		public async Task<List<TripData>> GetTripsByPULocationIdAsync (int puLocationId, DateTime? startDate = null, DateTime? endDate = null)
		{
			var query = _context.TripData
				.Where(t => t.PULocationID == puLocationId);

			if (startDate.HasValue)
			{
				query = query.Where(t => t.TpepPickupDatetime >= startDate.Value);
			}

			if (endDate.HasValue)
			{
				query = query.Where(t => t.TpepDropoffDatetime <= endDate.Value);
			}

			return await query.ToListAsync();
		}
	}
}
