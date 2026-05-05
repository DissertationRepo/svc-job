using AutoMapper;

namespace JobService.Api.Mappings
{
    public class RequirementsResponseMapping : Profile
    {
        public RequirementsResponseMapping()
        {
            CreateMap<Domain.Entities.ChildEntities.JobRequirement, Models.RequirementsResponse>()
                .ConvertUsing(src => new Models.RequirementsResponse
                {
                    Id = src.Id.ToString(),
                    JobId = src.JobId.Value.ToString(),
                    Description = src.Description,
                    Category = src.Category,
                    IsMandatory = src.IsMandatory,
                    CreatedAt = src.CreatedAt,
                    UpdatedAt = src.UpdatedAt
                });
        }
    }
}
