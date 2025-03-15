using Faces.Application.DTOs;
using Faces.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return base.Ok(_employeeService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return base.Ok(_employeeService.GetById(id));
        }

        [HttpPost()]
        public IActionResult Create([FromBody] EmployeeUpdateDto employeeUpdate)
        {
            try
            {
                _employeeService.Create(employeeUpdate);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create the employee", employeeUpdate);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] EmployeeUpdateDto employeeUpdate)
        {
            try
            {
                _employeeService.Update(employeeUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update the employee", employeeUpdate);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete the employee", id);
                return BadRequest(ex.Message);
            }
        }

    }
}
