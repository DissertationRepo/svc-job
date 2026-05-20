using JobService.Domain.Entities.Aggregates;

namespace JobService.Application.Abstract_Services
{
    public interface IJobRepository
    {
        Task AddJob(Job job, Guid userId);
        Task<Job?> GetJobById(Guid id);
        Task<ICollection<Job>> GetJobsByUserId(Guid userId);
        Task<bool> DeleteJobByIdAsync(Guid id, Guid userId);
        Task<ICollection<Job>> SearchJobsAsync(string query);
    }
}
