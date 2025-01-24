namespace DevelopsTodayTask.Interfaces
{
	public interface IFileProcessing<out T> where T : class
	{
		IAsyncEnumerable<T> GetStreamFile (string pathToFile);
	}
}
