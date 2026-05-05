using JobService.Application.Abstract_Services;
using JobService.Application.Models;
using JobService.Domain.Entities.ChildEntities;
using JobService.Domain.ValueObjects;

namespace JobService.Application.Services
{
    public class JobBenefitService : IJobBenefitService
    {
        private readonly IJobBenefitRepository _jobBenefitRepository;

        public JobBenefitService(IJobBenefitRepository jobBenefitRepository)
        {
            _jobBenefitRepository = jobBenefitRepository
                ?? throw new ArgumentNullException(nameof(jobBenefitRepository));
        }

        public async Task AddJobBenefitAsync(JobBenefitModel jobBenefitModel)
        {
            var domainBenefit = JobBenefit.Create(
                new JobId(Guid.Parse(jobBenefitModel.JobId)),
                jobBenefitModel.Name,
                jobBenefitModel.Description);

            await _jobBenefitRepository.AddBenefitAsync(domainBenefit);
        }

        public async Task<ICollection<JobBenefit>> GetJobBenefitsAsync(string jobId)
        {
            return await _jobBenefitRepository.GetBenefitsByJobIdAsync(Guid.Parse(jobId));
        }

        public async Task<bool> UpdateJobBenefitAsync(UpdateJobBenefitModel model)
        {
            return await _jobBenefitRepository.UpdateBenefitAsync(
                Guid.Parse(model.Id),
                model.Name,
                model.Description);
        }

        public async Task<bool> DeleteJobBenefitAsync(string benefitId)
        {
            return await _jobBenefitRepository.DeleteBenefitAsync(Guid.Parse(benefitId));
        }
    }
}
