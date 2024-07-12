using CampainScheduler.Application.CampainService.Interfaces;
using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CampainScheduler.Application.CampainService
{
    public class CampainService : ICampainService
    {
        private const int TopPriorityNumber = 1;

        private readonly ICampainSchedulerContext _context;
        private readonly ICampainConditionResolver _conditionResolver;

        public CampainService(
            ICampainSchedulerContext context,
            ICampainConditionResolver conditionResolver)
        {
            _context = context;
            _conditionResolver = conditionResolver;
        }

        #region Implementation of ICampainService

        public async Task<List<Customer>> GetCampainCustomersAsync(
            int campainId,
            CancellationToken cancellationToken = default)
        {
            var campain = await _context.Campains
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == campainId, cancellationToken)
                ?? throw new Exception($"Campain with id {campainId} doesn't exists");

            var prevCampainCustomerIds = await GetPrevCampainCustomerIdsAsync(
                campain,
                cancellationToken);

            return await _conditionResolver.GetCustomersByCampainConditionAsync(
                campain.CustomersCondition,
                prevCampainCustomerIds,
                cancellationToken);
        }
        

        #endregion

        #region Private Methods

        private async Task<List<int>> GetPrevCampainCustomerIdsAsync(
            Campain campain,
            CancellationToken cancellationToken = default)
        {
            if (campain.Priority == TopPriorityNumber)
            {
                return [];
            }

            var prevCampains = await _context.Campains
                .AsNoTracking()
                .Where(c => c.Priority < campain.Priority)
                .OrderBy(c => c.Priority)
                .ToListAsync(cancellationToken);

            if (!prevCampains.Any())
            {
                return [];
            }

            var topPriorityCampain = prevCampains
                .OrderBy(c => c.Priority)
                .First();

            var prevCampainCustomersIds = await GetCampainCustomersRecursiveAsync(
                    topPriorityCampain,
                    prevCampains,
                    [],
                    cancellationToken);

            return prevCampainCustomersIds;
        }

        private async Task<List<int>> GetCampainCustomersRecursiveAsync(
            Campain campain,
            List<Campain> campainList,
            List<int> customerIds,
            CancellationToken cancellationToken)
        {
            var compainCustomersIds = (await _conditionResolver
                    .GetCustomersByCampainConditionAsync(
                    campain.CustomersCondition,
                    customerIds,
                    cancellationToken))
                .Select(c => c.Id);

            customerIds.AddRange(compainCustomersIds);

            var nextCampain = campainList
                .FirstOrDefault(c => c.Priority == campain.Priority + 1);

            if (nextCampain == null)
            {
                return customerIds;
            }

            return await GetCampainCustomersRecursiveAsync(
                nextCampain,
                campainList,
                customerIds,
                cancellationToken);
        }

        #endregion
    }
}
