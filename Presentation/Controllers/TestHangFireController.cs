using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestHangFireController : BaseController
    {
        public TestHangFireController()
        {
        }

        [HttpGet]
        public void AddHangFire()
        {
            RecurringJob.AddOrUpdate("test", () => Console.WriteLine("HAHA"), Cron.Minutely);
        }
    }
}