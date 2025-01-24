using CsvHelper;
using DevelopsTodayTask.DbContext;
using DevelopsTodayTask.Models;
using DevelopsTodayTask.RawModels;
using System.Globalization;

namespace DevelopsTodayTask.Common
{
	public class InitialTripsSeeder
	{
		public static async Task ProcessTripsAndSaveDuplicatesAsync (IAsyncEnumerable<TripDataRaw> tripDataStream, string duplicatesFilePath)
		{
			using (var context = new TripDbContext())
			using (var writer = new StreamWriter(duplicatesFilePath))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteHeader<TripData>();
				await csv.NextRecordAsync();
				var tripsData = new List<TripData>();
				await foreach (var tripRaw in tripDataStream)
				{
					TripData tripData = new()
					{
						TpepPickupDatetime = tripRaw.TpepPickupDatetime,
						TpepDropoffDatetime = tripRaw.TpepDropoffDatetime,
						PassengerCount = tripRaw.PassengerCount,
						TripDistance = tripRaw.TripDistance,
						StoreAndFwdFlag = tripRaw.StoreAndFwdFlag.Trim().Contains("Y") ? "Yes" : "No",
						PULocationID = tripRaw.PULocationID,
						DOLocationID = tripRaw.DOLocationID,
						FareAmount = tripRaw.FareAmount,
						TipAmount = tripRaw.TipAmount
					};

					var existingRecord = tripsData
						.FirstOrDefault(t => t.TpepPickupDatetime == tripData.TpepPickupDatetime
						                     && t.TpepDropoffDatetime == tripData.TpepDropoffDatetime
						                     && t.PassengerCount == tripData.PassengerCount);

					if (existingRecord == null)
					{
						tripsData.Add(tripData);
					}
					else
					{
						csv.WriteRecord(tripRaw);
						await csv.NextRecordAsync();
					}
				}

				foreach (var tripData in tripsData)
				{
					tripData.TpepPickupDatetime = DateTimeConverter.ConvertEstToUtc(tripData.TpepPickupDatetime);
					tripData.TpepDropoffDatetime = DateTimeConverter.ConvertEstToUtc(tripData.TpepDropoffDatetime);
				}

				await context.TripData.AddRangeAsync(tripsData);
				await context.SaveChangesAsync();
			}
		}
	}
}
