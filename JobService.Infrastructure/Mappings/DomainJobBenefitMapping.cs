using AutoMapper;
using JobService.Domain.Entities.ChildEntities;
using JobService.Infrastructure.Entities;

namespace JobService.Infrastructure.Mappings
{
    public class DomainJobBenefitMapping : Profile
    {
        public DomainJobBenefitMapping()
        {
            CreateMap<JobBenefit, JobBenefitEntity>()
                .ConvertUsing(src => new JobBenefitEntity
                {
                    Id = src.Id,
                    JobId = src.JobId.Value,
                    Name = src.Name,
                    Description = src.Description,
                    CreatedAt = src.CreatedAt,
                    UpdatedAt = src.UpdatedAt
                });
        }
    }
}
