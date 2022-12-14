using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task AddHangFire() 
        {
            RecurringJob.AddOrUpdate("test", () => Console.WriteLine("HAHA"), Cron.Minutely);
        }
    }
}
