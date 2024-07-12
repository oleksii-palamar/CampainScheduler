using CampainScheduler.Application.Sender.Interfaces;

namespace CampainScheduler.Utils.FileWritter
{
    internal class SimpleFileWritter : IFileWritter
    {
        public async Task WriteAllLinesToFileAsync(
            string filePath,
            List<string> lines,
            CancellationToken cancellationToken = default)
            => await File.AppendAllLinesAsync(
                filePath,
                lines,
                cancellationToken: cancellationToken);
    }
}
