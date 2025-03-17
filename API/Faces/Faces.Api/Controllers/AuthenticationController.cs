using Faces.Authentication.DTOs;
using Faces.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Faces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        private readonly IAuthenticateService _authenticateService;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IAuthenticateService authenticateService)
        {
            _logger = logger;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Post(Credentials credentials)
        {

            try
            {
                var token = _authenticateService.AuthenticateByCredentials(credentials);
                return new ApiResponse<string>(HttpStatusCode.OK, string.Empty, token);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex, $"Unauthorized for {credentials.Document}");
                return new ApiResponse<string>(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation(ex, $"Employee not found of document {credentials.Document}");
                return new ApiResponse<string>(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                string message = $"Authentication Exception for {credentials.Document}";
                _logger.LogError(ex, message);
                return new ApiResponse<string>(HttpStatusCode.InternalServerError, message);
            }

        }

    }
}
