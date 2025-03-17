

using Faces.Application.Services;
using Faces.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Faces.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return new ApiResponse<JobFunction>(HttpStatusCode.OK, string.Empty, _jobFunctionService.GetAll());
        }
    }
}
