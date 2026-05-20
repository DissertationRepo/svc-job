using AutoMapper;
using AutoMapper;
using JobService.Application.Abstract_Services;
using JobService.Domain.Entities.Aggregates;
using JobService.Domain.ValueObjects;
using JobService.Infrastructure.Entities;
using JobService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task AddJob(Job job, Guid userId)
        {
            var jobEntity = _mapper.Map<JobEntity>(job);
            // set owner
            jobEntity.UserId = userId;
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

        public async Task<ICollection<Job>> GetJobsByUserId(Guid userId)
        {
            var jobEntities = await _db.Jobs
                .Where(j => j.UserId == userId)
                .ToListAsync();

            var result = jobEntities.Select(jobEntity => new Job(
                new JobId(jobEntity.Id),
                jobEntity.JobTitle,
                jobEntity.JobDescription,
                new SalaryRange(jobEntity.SalaryMin, jobEntity.SalaryMax, jobEntity.Currency),
                new Location(jobEntity.Location),
                new EmploymentType(jobEntity.EmploymentType),
                new RequiredSkill(jobEntity.RequiredSkillName, jobEntity.RequiredSkillLevel),
                new SeniorityLevel(jobEntity.SeniorityLevel)))
                .ToList();

            return result;
        }

        public async Task<ICollection<Job>> SearchJobsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Array.Empty<Job>();
            }

            var pattern = $"%{query.Trim()}%";

            var jobEntities = await _db.Jobs
                .Where(j => EF.Functions.ILike(j.JobTitle, pattern) || EF.Functions.ILike(j.JobDescription, pattern))
                .ToListAsync();

            var result = jobEntities.Select(jobEntity => new Job(
                new JobId(jobEntity.Id),
                jobEntity.JobTitle,
                jobEntity.JobDescription,
                new SalaryRange(jobEntity.SalaryMin, jobEntity.SalaryMax, jobEntity.Currency),
                new Location(jobEntity.Location),
                new EmploymentType(jobEntity.EmploymentType),
                new RequiredSkill(jobEntity.RequiredSkillName, jobEntity.RequiredSkillLevel),
                new SeniorityLevel(jobEntity.SeniorityLevel)))
                .ToList();

            return result;
        }

        public async Task<bool> DeleteJobByIdAsync(Guid id, Guid userId)
        {
            var jobEntity = await _db.Jobs.FirstOrDefaultAsync(j => j.Id == id);
            if (jobEntity == null)
            {
                return false;
            }

            if (jobEntity.UserId != userId)
            {
                // Not owner - do not delete
                return false;
            }

            _db.Jobs.Remove(jobEntity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
