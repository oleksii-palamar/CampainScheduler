using CampainScheduler.Application.CampainService.Interfaces;
using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Domain.Enums;
using CampainScheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CampainScheduler.Application.CampainService
{
    public class CampainConditionResolver : ICampainConditionResolver
    {
        private readonly ICampainSchedulerContext _context;

        public CampainConditionResolver(
            ICampainSchedulerContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersByCampainConditionAsync(
            string condition,
            List<int> customersToIgnore,
            CancellationToken cancellationToken = default)
        {
            var baseQuery = condition switch
            {
                "To all the Male customers" => GetAllMaleCusomers(),
                "To all customers above the age 45 " => GetAllCustomersAbove45(),
                "To all the customers from New York" => GetAllCustomersFromNewYork(),
                "To all the customers that deposit more than 100 $" => GetAllCustomersThatDepositMoreThen100(),
                "To all the customers that marked as new Customers " => GetAllNewCustomers(),
                _ => _context.Customers,
            };

            var queryWithoutIgnored = baseQuery
                .Where(c => !customersToIgnore.Contains(c.Id));

            return await queryWithoutIgnored
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Customer> GetAllNewCustomers() =>
            _context.Customers
                .Where(c => c.IsNewCusomer);

        private IQueryable<Customer> GetAllCustomersThatDepositMoreThen100() =>
            _context.Customers
                .Where(c => c.Deposit > 100);

        private IQueryable<Customer> GetAllCustomersFromNewYork() =>
            _context.Customers
                .Where(c => c.City == "New York");

        private IQueryable<Customer> GetAllCustomersAbove45() =>
            _context.Customers
                .Where(c => c.Age > 45);

        private IQueryable<Customer> GetAllMaleCusomers() =>
             _context.Customers
                .Where(c => c.Gender == CustomerGender.Male);
    }
}
