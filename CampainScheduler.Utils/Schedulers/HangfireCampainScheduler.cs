using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Application.Scheduler.Interfaces;
using CampainScheduler.Application.Sender.Interfaces;
using CampainScheduler.Utils.Cron.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace CampainScheduler.Utils.Schedulers
{
    internal class HangfireCampainScheduler : ICampainScheduler
    {
        private readonly ICampainSchedulerContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ICampainSender _campainSender;
        private readonly ICronConvertor _cronConvertor;

        public HangfireCampainScheduler(
            ICampainSchedulerContext context,
            IBackgroundJobClient backgroundJobClient,
            ICampainSender campainSender,
            ICronConvertor cronConvertor)
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient;
            _campainSender = campainSender;
            _cronConvertor = cronConvertor;
        }

        public async Task ScheduleCampainAsync(
            int campainId,
            CancellationToken cancellationToken = default) 
        {
            var campain = await _context.Campains
                .FirstOrDefaultAsync(x => x.Id == campainId, cancellationToken)
                ?? throw new Exception($"Campain with id {campainId} not found");

            RecurringJob.AddOrUpdate(
                $"campain-{campainId}", 
                () => _campainSender.SendCampainsByIdsAsync(new List<int> { campainId }, cancellationToken),
                "* * * * *",//_cronConvertor.TimeSpanToCron(campain.ScheduledTimeUtc),
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Utc
                });
        }
    }
}
