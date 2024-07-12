using CampainScheduler.Domain.Models;

namespace CampainScheduler.Application.CampainService.Interfaces
{
    public interface ICampainConditionResolver
    {
        Task<List<Customer>> GetCustomersByCampainConditionAsync(
            string condition,
            List<int> customersToIgnore = null,
            CancellationToken cancellationToken = default);
    }
}
