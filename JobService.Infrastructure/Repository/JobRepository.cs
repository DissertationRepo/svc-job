using AutoMapper;
using JobService.Application.Abstract_Services;
using JobService.Domain.Entities.Aggregates;
using JobService.Domain.ValueObjects;
using JobService.Infrastructure.Entities;
using JobService.Infrastructure.Persistence;

namespace JobService.Infrastructure.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext _db;
        private readonly IMapper _mapper;

        public JobRepository(JobDbContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddJob(Job job)
        {
            var jobEntity = _mapper.Map<JobEntity>(job);
            try
            {
                await _db.Jobs.AddAsync(jobEntity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the job. Exception Message: {ex.Message}", ex);
            }
        }

        public async Task<Job?> GetJobById(Guid id)
        {
            var jobEntity = await _db.Jobs.FindAsync(id);
            if (jobEntity == null)
            {
                return null;
            }

            var domainJob = new Job(
                new JobId(jobEntity.Id),
                jobEntity.JobTitle,
                jobEntity.JobDescription,
                new SalaryRange(jobEntity.SalaryMin, jobEntity.SalaryMax, jobEntity.Currency),
                new Location(jobEntity.Location),
                new EmploymentType(jobEntity.EmploymentType),
                new RequiredSkill(jobEntity.RequiredSkillName, jobEntity.RequiredSkillLevel),
                new SeniorityLevel(jobEntity.SeniorityLevel));

            return domainJob;
        }
    }
}
