namespace CampainScheduler.Utils.CsvParsers.Interfaces
{
    public interface ICsvParser<T>
    {
        List<T> Parse(string fileName);
    }
}
