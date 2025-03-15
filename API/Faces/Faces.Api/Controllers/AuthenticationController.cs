using Faces.Authentication.DTOs;
using Faces.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex, $"Unauthorized for {credentials.Document}");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Authentication Exception for {credentials.Document}");
                throw;
            }

        }

    }
}
