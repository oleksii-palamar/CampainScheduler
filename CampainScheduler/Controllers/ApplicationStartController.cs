using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Application.Scheduler.Interfaces;
using CampainScheduler.Application.Sender.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampainScheduler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationStartController : ControllerBase
    {
        private readonly ICampainSender _sender;
        private readonly ICampainScheduler _scheduler;

        public ApplicationStartController(
            ICampainSchedulerContext context,
            ICampainSender sender,
            ICampainScheduler scheduler)
        {
            _sender = sender;
            _scheduler = scheduler;
        }

        [HttpGet]
        public async Task Get(CancellationToken cancellationToken)
        {
            await _scheduler.ScheduleCampainAsync(1, cancellationToken);
            await _scheduler.ScheduleCampainAsync(2, cancellationToken);
            await _scheduler.ScheduleCampainAsync(3, cancellationToken);
            await _scheduler.ScheduleCampainAsync(4, cancellationToken);
            await _scheduler.ScheduleCampainAsync(5, cancellationToken);
        }
    }
}
