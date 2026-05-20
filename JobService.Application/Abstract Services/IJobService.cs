using JobService.Application.Models;
using JobService.Domain.Entities.Aggregates;

namespace JobService.Application.Abstract_Services
{
    public interface IJobService
    {
        Task CreateJobAsync(JobModel newJob, Guid userId);
        Task<Job?> GetJobByIdAsync(Guid id);
        Task<ICollection<Job>> GetJobsByUserAsync(Guid userId);
        Task<bool> DeleteJobAsync(Guid id, Guid userId);
        Task<ICollection<Job>> SearchJobsAsync(string query);
    }
}
