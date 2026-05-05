using JobService.Application.Abstract_Services;
using JobService.Application.Models;
using JobService.Domain.Entities.ChildEntities;
using JobService.Domain.ValueObjects;

namespace JobService.Application.Services
{
    public class JobRequirementService : IJobRequirementService
    {
        private readonly IJobRequirementRepository _jobRequirementRepository;

        public JobRequirementService(IJobRequirementRepository jobRequirementRepository)
        {
            _jobRequirementRepository = jobRequirementRepository
                ?? throw new ArgumentNullException(nameof(jobRequirementRepository));
        }

        public async Task AddJobRequirementAsync(JobRequirementModel jobRequirementModel)
        {
            var domainRequirement = JobRequirement.Create(
                new JobId(Guid.Parse(jobRequirementModel.JobId)),
                jobRequirementModel.Description,
                jobRequirementModel.Category,
                jobRequirementModel.IsMandatory);

            await _jobRequirementRepository.AddRequirementAsync(domainRequirement);
        }

        public async Task<ICollection<JobRequirement>> GetJobRequirementsAsync(string jobId)
        {
            return await _jobRequirementRepository.GetRequirementsByJobIdAsync(Guid.Parse(jobId));
        }

        public async Task<bool> UpdateJobRequirementAsync(UpdateJobRequirementModel model)
        {
            return await _jobRequirementRepository.UpdateRequirementAsync(
                Guid.Parse(model.Id),
                model.Description,
                model.Category,
                model.IsMandatory);
        }

        public async Task<bool> DeleteJobRequirementAsync(string requirementId)
        {
            return await _jobRequirementRepository.DeleteRequirementAsync(Guid.Parse(requirementId));
        }
    }
}
