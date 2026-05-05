using JobService.Domain.Entities.ChildEntities;

namespace JobService.Application.Abstract_Services
{
    public interface IJobRequirementRepository
    {
        Task AddRequirementAsync(JobRequirement requirement);
        Task<ICollection<JobRequirement>> GetRequirementsByJobIdAsync(Guid jobId);
        Task<bool> UpdateRequirementAsync(
            Guid requirementId,
            string description,
            string? category,
            bool isMandatory);
        Task<bool> DeleteRequirementAsync(Guid requirementId);
    }
}
