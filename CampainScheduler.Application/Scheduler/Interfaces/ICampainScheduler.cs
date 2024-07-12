namespace CampainScheduler.Application.Scheduler.Interfaces
{
    public interface ICampainScheduler
    {
        Task ScheduleCampainAsync(
            int campainId,
            CancellationToken cancellationToken = default);
    }
}
