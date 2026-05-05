using AutoMapper;
using JobService.Api.Models;

namespace JobService.Api.Mappings
{
    public class AddJobRequirementMapping : Profile
    {
        public AddJobRequirementMapping()
        {
            CreateMap<AddJobRequirement, Application.Models.JobRequirementModel>()
                .ConvertUsing(src => new Application.Models.JobRequirementModel(
                    src.JobId!,
                    src.Description!,
                    src.IsMandatory)
                {
                    Category = src.Category
                });
        }
    }
}
