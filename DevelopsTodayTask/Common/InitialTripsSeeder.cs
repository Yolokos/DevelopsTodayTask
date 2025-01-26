using CsvHelper;
using DevelopsTodayTask.DbContext;
using DevelopsTodayTask.Models;
using DevelopsTodayTask.RawModels;
using EFCore.BulkExtensions;
using System.Collections.Concurrent;
using System.Globalization;

namespace DevelopsTodayTask.Common
{
	public class InitialTripsSeeder
	{
		public static async Task ProcessTripsAndSaveDuplicatesAsync (IAsyncEnumerable<TripDataRaw> tripDataStream, string duplicatesFilePath)
		{

			var tripsData = new List<TripData>();
			var duplicateRecords = new ConcurrentBag<TripDataRaw>();
			var processedTrips = new ConcurrentDictionary<string, TripData>();
			await Parallel.ForEachAsync(tripDataStream, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (tripRaw, token) =>
			{
				var tripKey = $"{tripRaw.TpepPickupDatetime}_{tripRaw.TpepDropoffDatetime}_{tripRaw.PassengerCount}";

				TripData tripData = new()
				{
					TpepPickupDatetime = DateTimeConverter.ConvertEstToUtc(tripRaw.TpepPickupDatetime),
					TpepDropoffDatetime = DateTimeConverter.ConvertEstToUtc(tripRaw.TpepDropoffDatetime),
					PassengerCount = tripRaw.PassengerCount,
					TripDistance = tripRaw.TripDistance,
					StoreAndFwdFlag = tripRaw.StoreAndFwdFlag.Trim().Contains("Y") ? "Yes" : "No",
					PULocationID = tripRaw.PULocationID,
					DOLocationID = tripRaw.DOLocationID,
					FareAmount = tripRaw.FareAmount,
					TipAmount = tripRaw.TipAmount
				};

				if (!processedTrips.TryAdd(tripKey, tripData))
				{
					duplicateRecords.Add(tripRaw);
				}

				await Task.CompletedTask; 
			});

			using var context = new TripDbContext();
			using var writer = new StreamWriter(duplicatesFilePath);
			using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
			csv.WriteHeader<TripDataRaw>();
			await csv.NextRecordAsync();

			foreach (var duplicate in duplicateRecords)
			{
				csv.WriteRecord(duplicate);
				await csv.NextRecordAsync();
			}

			await context.BulkInsertAsync(processedTrips.Values.ToList());
		}
	}
}
