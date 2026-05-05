using AutoMapper;
using JobService.Application.Abstract_Services;
using JobService.Domain.Entities.ChildEntities;
using JobService.Domain.ValueObjects;
using JobService.Infrastructure.Entities;
using JobService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobService.Infrastructure.Repository
{
    public class JobRequirementRepository : IJobRequirementRepository
    {
        private readonly JobDbContext _db;
        private readonly IMapper _mapper;

        public JobRequirementRepository(JobDbContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddRequirementAsync(JobRequirement requirement)
        {
            var entity = _mapper.Map<JobRequirementEntity>(requirement);
            try
            {
                await _db.JobRequirements.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the job requirement.", ex);
            }
        }

        public async Task<ICollection<JobRequirement>> GetRequirementsByJobIdAsync(Guid jobId)
        {
            var entities = await _db.JobRequirements.Where(r => r.JobId == jobId).ToListAsync();
            var requirements = new List<JobRequirement>();
            foreach (var entity in entities)
            {
                requirements.Add(JobRequirement.Load(
                    entity.Id,
                    new JobId(entity.JobId),
                    entity.Description,
                    entity.Category,
                    entity.IsMandatory,
                    entity.CreatedAt,
                    entity.UpdatedAt));
            }
            return requirements;
        }

        public async Task<bool> UpdateRequirementAsync(
            Guid requirementId,
            string description,
            string? category,
            bool isMandatory)
        {
            var entity = await _db.JobRequirements.FirstOrDefaultAsync(r => r.Id == requirementId);
            if (entity is null)
            {
                return false;
            }

            var domainRequirement = JobRequirement.Load(
                entity.Id,
                new JobId(entity.JobId),
                entity.Description,
                entity.Category,
                entity.IsMandatory,
                entity.CreatedAt,
                entity.UpdatedAt);

            domainRequirement.Update(description, category, isMandatory);

            entity.Description = domainRequirement.Description;
            entity.Category = domainRequirement.Category;
            entity.IsMandatory = domainRequirement.IsMandatory;
            entity.UpdatedAt = domainRequirement.UpdatedAt;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRequirementAsync(Guid requirementId)
        {
            var entity = await _db.JobRequirements.FirstOrDefaultAsync(r => r.Id == requirementId);
            if (entity is null)
            {
                return false;
            }

            _db.JobRequirements.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
