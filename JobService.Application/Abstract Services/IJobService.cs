using JobService.Application.Models;
using JobService.Domain.Entities.Aggregates;

namespace JobService.Application.Abstract_Services
{
    public interface IJobService
    {
        Task CreateJobAsync(JobModel newJob);
        Task<Job?> GetJobByIdAsync(Guid id);
    }
}
