using DevelopsTodayTask.Common;
using DevelopsTodayTask.DbContext;
using DevelopsTodayTask.Processing;
using DevelopsTodayTask.Repositories;

namespace DevelopsTodayTask
{
	internal class Program
	{
		private const string TripsDataPath = @"Data\sample-cab-data.csv";
		private const string DuplicatesDataPath = @"Data\duplicates.csv";

		static async Task Main (string[] args)
		{
			TripDbContext _context = new();

			TripDataCsvProcessing tripDataCsvProccessor = new();
			TripRepository tripRepository = new(_context);
			TripQueryRepository tripQueryRepository = new(_context);

			if (tripRepository.Count() == 0)
			{
				var tripDataList = tripDataCsvProccessor.GetStreamFile(TripsDataPath);

				await InitialTripsSeeder.ProcessTripsAndSaveDuplicatesAsync(tripDataList, DuplicatesDataPath);
			}

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Choose operation:");
				Console.WriteLine("1. Find out which `PULocationId` (Pick-up location ID) has the highest tip_amount on average.");
				Console.WriteLine("2. Find the top 100 longest fares in terms of `trip_distance`.");
				Console.WriteLine("3. Find the top 100 longest fares in terms of time spent traveling.");
				Console.WriteLine("4. Search, where part of the conditions is `PULocationId`.");
				Console.WriteLine("0. Exit");

				var input = Console.ReadLine();

				if (input == "0")
				{
					break; 
				}

				switch (input)
				{
					case "1":
						var getTripWithHighestTip = await tripQueryRepository.GetPuLocationIdWithHighestAverageTipAsync();
						Console.WriteLine($"PULocationId with the highest tip_amount on average: {getTripWithHighestTip}");
						break;

					case "2":
						var getLongestTripsByDistance = await tripQueryRepository.GetLongestTripsAsync();
						Console.WriteLine("100 longest fares in terms of `trip_distance`:");
						foreach (var trip in getLongestTripsByDistance)
						{
							Console.WriteLine($"TripId: {trip.Id}, Distance: {trip.TripDistance}");
						}
						break;

					case "3":
						var getLongestTripsByDuration = await tripQueryRepository.GetLongestTripsByDurationAsync();
						Console.WriteLine("100 longest fares in terms of time spent traveling:");
						foreach (var trip in getLongestTripsByDuration)
						{
							Console.WriteLine($"TripId: {trip.Id}, Duration: {(trip.TpepDropoffDatetime - trip.TpepPickupDatetime).TotalMinutes} минут");
						}
						break;

					case "4":
						Console.Write("Enter PULocationId: ");
						if (!int.TryParse(Console.ReadLine(), out var pulocationId))
						{
							Console.WriteLine("Invalid PULocationId. Try again.");
							break;
						}

						DateTime startDate, endDate;

						while (true)
						{
							Console.Write("Enter start date (yyyy-MM-dd HH:mm:ss): ");
							var startDateInput = Console.ReadLine();
							if (DateTime.TryParse(startDateInput, out startDate))
							{
								break;
							}
							else
							{
								Console.WriteLine("Invalid format date. Try again.");
							}
						}

						while (true)
						{
							Console.Write("Enter end date (yyyy-MM-dd HH:mm:ss): ");
							var endDateInput = Console.ReadLine();
							if (DateTime.TryParse(endDateInput, out endDate))
							{
								break;
							}
							else
							{
								Console.WriteLine("Invalid format date. Try again.");
							}
						}

						var searchTrips = await tripQueryRepository.GetTripsByPULocationIdAsync(pulocationId, startDate, endDate);
						Console.WriteLine($"Trips with PULocationId {pulocationId} from {startDate} to {endDate}:");
						foreach (var trip in searchTrips)
						{
							Console.WriteLine($"TripId: {trip.Id}, Distance: {trip.TripDistance}, Pickup: {trip.TpepPickupDatetime}, Dropoff: {trip.TpepDropoffDatetime}");
						}
						break;

					default:
						Console.WriteLine("Invalid operation. Try again.");
						break;
				}

				Console.WriteLine("\nPress any button for continue...");
				Console.ReadKey();
			}
		}
	}
}