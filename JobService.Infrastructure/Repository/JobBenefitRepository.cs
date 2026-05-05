using AutoMapper;
using JobService.Application.Abstract_Services;
using JobService.Domain.Entities.ChildEntities;
using JobService.Domain.ValueObjects;
using JobService.Infrastructure.Entities;
using JobService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobService.Infrastructure.Repository
{
    public class JobBenefitRepository : IJobBenefitRepository
    {
        private readonly JobDbContext _db;
        private readonly IMapper _mapper;

        public JobBenefitRepository(JobDbContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddBenefitAsync(JobBenefit benefit)
        {
            var entity = _mapper.Map<JobBenefitEntity>(benefit);
            try
            {
                await _db.JobBenefits.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the job benefit.", ex);
            }
        }

        public async Task<ICollection<JobBenefit>> GetBenefitsByJobIdAsync(Guid jobId)
        {
            var entities = await _db.JobBenefits.Where(b => b.JobId == jobId).ToListAsync();
            var benefits = new List<JobBenefit>();
            foreach (var entity in entities)
            {
                benefits.Add(JobBenefit.Load(
                    entity.Id,
                    new JobId(entity.JobId),
                    entity.Name,
                    entity.Description,
                    entity.CreatedAt,
                    entity.UpdatedAt));
            }
            return benefits;
        }

        public async Task<bool> UpdateBenefitAsync(Guid benefitId, string name, string? description)
        {
            var entity = await _db.JobBenefits.FirstOrDefaultAsync(b => b.Id == benefitId);
            if (entity is null)
            {
                return false;
            }

            var domainBenefit = JobBenefit.Load(
                entity.Id,
                new JobId(entity.JobId),
                entity.Name,
                entity.Description,
                entity.CreatedAt,
                entity.UpdatedAt);

            domainBenefit.Update(name, description);

            entity.Name = domainBenefit.Name;
            entity.Description = domainBenefit.Description;
            entity.UpdatedAt = domainBenefit.UpdatedAt;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBenefitAsync(Guid benefitId)
        {
            var entity = await _db.JobBenefits.FirstOrDefaultAsync(b => b.Id == benefitId);
            if (entity is null)
            {
                return false;
            }

            _db.JobBenefits.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
