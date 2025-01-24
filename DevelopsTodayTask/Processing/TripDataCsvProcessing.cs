using CsvHelper.Configuration;
using CsvHelper;
using DevelopsTodayTask.Interfaces;
using System.Globalization;
using DevelopsTodayTask.RawModels;

namespace DevelopsTodayTask.Processing
{
	public class TripDataCsvProcessing : IFileProcessing<TripDataRaw>
	{
		public async IAsyncEnumerable<TripDataRaw> GetStreamFile (string pathToFile)
		{
			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				HasHeaderRecord = true,
			};

			using var reader = new StreamReader(pathToFile);
			using var csv = new CsvReader(reader, config);

			await foreach (var record in csv.GetRecordsAsync<TripDataRaw>())
			{
				yield return record;
			}
		}
	}
}
