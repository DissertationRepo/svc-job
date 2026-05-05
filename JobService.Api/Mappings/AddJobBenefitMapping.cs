using AutoMapper;
using JobService.Api.Models;

namespace JobService.Api.Mappings
{
    public class AddJobBenefitMapping : Profile
    {
        public AddJobBenefitMapping()
        {
            CreateMap<AddJobBenefit, Application.Models.JobBenefitModel>()
                .ConvertUsing(src => new Application.Models.JobBenefitModel(
                    src.JobId!,
                    src.Name!)
                {
                    Description = src.Description
                });
        }
    }
}
