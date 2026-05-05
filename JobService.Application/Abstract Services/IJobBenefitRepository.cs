using JobService.Domain.Entities.ChildEntities;

namespace JobService.Application.Abstract_Services
{
    public interface IJobBenefitRepository
    {
        Task AddBenefitAsync(JobBenefit benefit);
        Task<ICollection<JobBenefit>> GetBenefitsByJobIdAsync(Guid jobId);
        Task<bool> UpdateBenefitAsync(Guid benefitId, string name, string? description);
        Task<bool> DeleteBenefitAsync(Guid benefitId);
    }
}
