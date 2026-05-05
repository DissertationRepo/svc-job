using AutoMapper;
using JobService.Domain.Entities.ChildEntities;
using JobService.Infrastructure.Entities;

namespace JobService.Infrastructure.Mappings
{
    public class DomainJobRequirementMapping : Profile
    {
        public DomainJobRequirementMapping()
        {
            CreateMap<JobRequirement, JobRequirementEntity>()
                .ConvertUsing(src => new JobRequirementEntity
                {
                    Id = src.Id,
                    JobId = src.JobId.Value,
                    Description = src.Description,
                    Category = src.Category,
                    IsMandatory = src.IsMandatory,
                    CreatedAt = src.CreatedAt,
                    UpdatedAt = src.UpdatedAt
                });
        }
    }
}
