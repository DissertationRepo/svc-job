using JobService.Application.Models;
using JobService.Domain.Entities.ChildEntities;

namespace JobService.Application.Abstract_Services
{
    public interface IJobBenefitService
    {
        Task AddJobBenefitAsync(JobBenefitModel jobBenefitModel);
        Task<ICollection<JobBenefit>> GetJobBenefitsAsync(string jobId);
        Task<bool> UpdateJobBenefitAsync(UpdateJobBenefitModel jobBenefitModel);
        Task<bool> DeleteJobBenefitAsync(string benefitId);
    }
}
