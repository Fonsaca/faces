using Faces.Database.EF;
using Faces.Domain.Entities;

namespace Faces.Database.Repositories
{
    public interface IJobFunctionRepository
    {
        List<JobFunction> GetAll();

        JobFunction? GetByCode(string code);
    }

    internal class JobFunctionRepository : IJobFunctionRepository
    {

        private readonly FacesDbContext _context;

        public JobFunctionRepository(FacesDbContext context)
        {
            _context = context;
        }


        public List<JobFunction> GetAll()
        {
            return _context.JobFunctions
                .Select(x => x.ConvertToDomain())
                .ToList();
        }

        public JobFunction? GetByCode(string code)
        {
            return _context.JobFunctions
                 .Where(x => x.Code.Equals(code))
                 .Select(x => x.ConvertToDomain())
                 .FirstOrDefault();
        }
    }
}
