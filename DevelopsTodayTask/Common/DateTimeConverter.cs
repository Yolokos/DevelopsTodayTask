namespace DevelopsTodayTask.Common
{
	public static class DateTimeConverter
	{
		public static DateTime ConvertEstToUtc (DateTime estDateTime)
		{
			var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(estDateTime, estTimeZone);

			return utcDateTime;
		}
	}
}
