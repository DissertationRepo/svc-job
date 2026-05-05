using JobService.Domain.Entities.Aggregates;

namespace JobService.Application.Abstract_Services
{
    public interface IJobRepository
    {
        Task AddJob(Job job);
        Task<Job?> GetJobById(Guid id);
    }
}
