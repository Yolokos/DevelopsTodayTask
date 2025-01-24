using DevelopsTodayTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DevelopsTodayTask.DbContext
{
	public class TripDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public DbSet<TripData> TripData { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=trips.db");
		}

		protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TripData>().ToTable("Trips");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => new { t.TpepPickupDatetime, t.TpepDropoffDatetime, t.PassengerCount })
				.IsUnique();

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.PULocationID)
				.HasDatabaseName("idx_pulocation_id");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.DOLocationID)
				.HasDatabaseName("idx_dolocation_id");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.TpepPickupDatetime)
				.HasDatabaseName("idx_pickup_datetime");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.TpepDropoffDatetime)
				.HasDatabaseName("idx_dropoff_datetime");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.TripDistance)
				.HasDatabaseName("idx_trip_distance");

			modelBuilder.Entity<TripData>()
				.HasIndex(t => t.TipAmount)
				.HasDatabaseName("idx_tip_amount");
		}
	}
}
