using AutoMapper;
using JobService.Api.Models;

namespace JobService.Api.Mappings
{
    public class UpdateJobBenefitMapping : Profile
    {
        public UpdateJobBenefitMapping()
        {
            CreateMap<UpdateJobBenefit, Application.Models.UpdateJobBenefitModel>()
                .ConvertUsing(src => new Application.Models.UpdateJobBenefitModel(
                    src.Id!,
                    src.Name!)
                {
                    Description = src.Description
                });
        }
    }
}
