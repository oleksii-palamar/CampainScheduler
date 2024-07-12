namespace CampainScheduler.Application.Sender.Interfaces
{
    public interface IFileWritter
    {
        Task WriteAllLinesToFileAsync(
            string filePath,
            List<string> lines,
            CancellationToken cancellationToken = default);
    }
}
