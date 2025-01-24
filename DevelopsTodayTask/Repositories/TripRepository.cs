using DevelopsTodayTask.DbContext;
using DevelopsTodayTask.Repositories.Interfaces;

namespace DevelopsTodayTask.Repositories
{
	public class TripRepository : ITripRepository
	{
		private readonly TripDbContext _context;

		public TripRepository(TripDbContext context)
		{
			_context = context;
		}

		public int Count()
		{
			return _context.TripData.Count();
		}
	}
}
