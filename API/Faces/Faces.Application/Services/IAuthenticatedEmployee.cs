using Faces.Domain.Entities;

namespace Faces.Application.Services
{
    public interface IAuthenticatedEmployee
    {

        bool IsAuthenticated { get; }

        Employee? Employee { get; }
    }
}
