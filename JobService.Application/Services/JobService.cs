using JobService.Application.Abstract_Services;
using JobService.Application.Models;
using JobService.Domain.Entities.Aggregates;

namespace JobService.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public async Task CreateJobAsync(JobModel newJob)
        {
            var domainJob = Job.Create(
                newJob.JobTitle,
                newJob.JobDescription,
                newJob.SalaryMin,
                newJob.SalaryMax,
                newJob.Currency,
                newJob.Location,
                newJob.EmploymentType,
                newJob.RequiredSkillName,
                newJob.RequiredSkillLevel,
                newJob.SeniorityLevel);

            await _jobRepository.AddJob(domainJob);
        }

        public Task<Job?> GetJobByIdAsync(Guid id)
        {
            return _jobRepository.GetJobById(id);
        }
    }
}
