

using Faces.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Faces.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class JobFunctionController : ControllerBase
    {

        private readonly ILogger<JobFunctionController> _logger;

        private readonly IJobFunctionService _jobFunctionService;

        public JobFunctionController(ILogger<JobFunctionController> logger, IJobFunctionService jobFunctionService)
        {
            _logger = logger;
            _jobFunctionService = jobFunctionService;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return base.Ok(_jobFunctionService.GetAll());
        }
    }
}
