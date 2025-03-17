using Faces.Application.DTOs;
using Faces.Application.Services;
using Faces.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

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
            return new ApiResponse<Employee>(HttpStatusCode.OK, string.Empty, _employeeService.GetAll());
        }

        [HttpGet("{document}")]
        public IActionResult GetByDocument(string document)
        {

            return new ApiResponse<Employee?>(HttpStatusCode.OK, string.Empty, _employeeService.GetByDocument(document));
        }

        [HttpPost()]
        public IActionResult Create([FromBody] EmployeeUpdateDto employeeUpdate)
        {
            try
            {
                _employeeService.Create(employeeUpdate);
                return new ApiResponse<object>(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create the employee", employeeUpdate);
                return new ApiResponse<object>(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] EmployeeUpdateDto employeeUpdate)
        {
            try
            {
                _employeeService.Update(employeeUpdate);
                return new ApiResponse<object>(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update the employee", employeeUpdate);
                return new ApiResponse<object>(HttpStatusCode.BadRequest, ex.Message);
 
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return new ApiResponse<object>(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete the employee", id);
                return new ApiResponse<object>(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
