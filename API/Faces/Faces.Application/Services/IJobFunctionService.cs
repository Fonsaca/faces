using Faces.Database.Repositories;
using Faces.Domain.Entities;

namespace Faces.Application.Services
{
    public interface IJobFunctionService
    {

        List<JobFunction> GetAll();

    }

    public class JobFunctionService : IJobFunctionService
    {

        private readonly IJobFunctionRepository _jobFunctionRepository;

        public JobFunctionService(IJobFunctionRepository jobFunctionRepository)
        {
            _jobFunctionRepository = jobFunctionRepository;
        }

        public List<JobFunction> GetAll()
        {
            return _jobFunctionRepository.GetAll();
        }
    }
}
