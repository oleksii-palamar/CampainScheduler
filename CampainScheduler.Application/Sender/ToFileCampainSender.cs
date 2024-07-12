using CampainScheduler.Application.CampainService.Interfaces;
using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Application.Sender.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CampainScheduler.Application.Sender
{
    public class ToFileCampainSender : ICampainSender
    {
        private const string SendFileLocation = @"../CampainScheduler/";

        private readonly ICampainService _campainCustomerService;
        private readonly ICampainSchedulerContext _context;
        private readonly IConfiguration _configuration;
        private readonly IFileWritter _fileWritter;

        public ToFileCampainSender(
            ICampainService campainService,
            ICampainSchedulerContext context,
            IConfiguration configuration,
            IFileWritter fileWritter)
        {
            _campainCustomerService = campainService;
            _context = context;
            _configuration = configuration;
            _fileWritter = fileWritter;
        }

        public async Task SendCampainsByIdsAsync(
            List<int> campainIds,
            CancellationToken cancellationToken = default)
        {
            if (!campainIds.Any())
            {
                return;
            }

            var campains = await _context.Campains
                .AsNoTracking()
                .Include(c => c.Template)
                .Where(c => campainIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var customersList = new List<string>();

            foreach (var campain in campains)
            {
                var customers = await _campainCustomerService
                    .GetCampainCustomersAsync(campain.Id);

                foreach (var customer in customers)
                {
                    customersList.Add($"Customer with Id {customer.Id} received template with id {campain.TemplateId}");
                }
            }

            if (!customersList.Any())
            {
                return;
            }

            var sendFileFullPath = SendFileLocation + _configuration["CampainSender:OutputFile"];

            await _fileWritter.WriteAllLinesToFileAsync(
                sendFileFullPath,
                customersList,
                cancellationToken);
        }
    }
}
