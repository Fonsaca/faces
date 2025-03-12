using Faces.Domain.Entities;

namespace Faces.Database.Repositories
{
    public interface IJobFunctionRepository
    {
        List<JobFunction> GetAll();
        JobFunction GetByCode(string code);
    }

    internal class JobFunctionRepository : IJobFunctionRepository
    {
        public List<JobFunction> GetAll()
        {
            return new List<JobFunction>()
            {
                new JobFunction("0001","Analyst",10),
                new JobFunction("0002","CEO",JobFunction.HIGH_HIERARCHY),
                new JobFunction("0003","Tech Leader",100)
            }; //TODO get from database
        }

        public JobFunction GetByCode(string code)
        {
            return GetAll().FirstOrDefault(x => x.Code.Equals(code)); //TODO get from database
        }
    }
}
