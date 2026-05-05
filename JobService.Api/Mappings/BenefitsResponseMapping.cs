using AutoMapper;

namespace JobService.Api.Mappings
{
    public class BenefitsResponseMapping : Profile
    {
        public BenefitsResponseMapping()
        {
            CreateMap<Domain.Entities.ChildEntities.JobBenefit, Models.BenefitsResponse>()
                .ConvertUsing(src => new Models.BenefitsResponse
                {
                    Id = src.Id.ToString(),
                    JobId = src.JobId.Value.ToString(),
                    Name = src.Name,
                    Description = src.Description,
                    CreatedAt = src.CreatedAt,
                    UpdatedAt = src.UpdatedAt
                });
        }
    }
}
