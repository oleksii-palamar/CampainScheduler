using CampainScheduler.Domain.Models;

namespace CampainScheduler.Application.CampainService.Interfaces
{
    public interface ICampainService
    {
        Task<List<Customer>> GetCampainCustomersAsync(
            int campainId,
            CancellationToken cancellationToken = default);
    }
}
