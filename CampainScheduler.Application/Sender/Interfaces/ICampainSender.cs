namespace CampainScheduler.Application.Sender.Interfaces
{
    public interface ICampainSender
    {
        Task SendCampainsByIdsAsync(
            List<int> campainIds,
            CancellationToken cancellationToken = default);
    }
}
