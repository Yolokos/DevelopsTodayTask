using DevelopsTodayTask.Models;

namespace DevelopsTodayTask.Repositories.Interfaces
{
	public interface ITripQueryRepository
	{
		Task<(int? PULocationID, double? AverageTip)> GetPuLocationIdWithHighestAverageTipAsync();
		Task<List<TripData>> GetLongestTripsAsync();
		Task<List<TripData>> GetLongestTripsByDurationAsync();
		Task<List<TripData>> GetTripsByPULocationIdAsync(int puLocationId, DateTime? startDate = null,
			DateTime? endDate = null);
	}
}
