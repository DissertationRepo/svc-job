using JobService.Application.Models;
using JobService.Domain.Entities.ChildEntities;

namespace JobService.Application.Abstract_Services
{
    public interface IJobRequirementService
    {
        Task AddJobRequirementAsync(JobRequirementModel jobRequirementModel);
        Task<ICollection<JobRequirement>> GetJobRequirementsAsync(string jobId);
        Task<bool> UpdateJobRequirementAsync(UpdateJobRequirementModel jobRequirementModel);
        Task<bool> DeleteJobRequirementAsync(string requirementId);
    }
}
