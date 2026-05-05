using AutoMapper;
using JobService.Api.Models;

namespace JobService.Api.Mappings
{
    public class UpdateJobRequirementMapping : Profile
    {
        public UpdateJobRequirementMapping()
        {
            CreateMap<UpdateJobRequirement, Application.Models.UpdateJobRequirementModel>()
                .ConvertUsing(src => new Application.Models.UpdateJobRequirementModel(
                    src.Id!,
                    src.Description!,
                    src.IsMandatory)
                {
                    Category = src.Category
                });
        }
    }
}
