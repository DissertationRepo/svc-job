using AutoMapper;
using JobService.Api.Models;
using JobService.Application.Models;

namespace JobService.Api.Mappings
{
    public class NewJobMapping : Profile
    {
        public NewJobMapping()
        {
            CreateMap<NewJob, JobModel>()
                .ConstructUsing(src => CreateApplicationNewJob(src));
        }

        private JobModel CreateApplicationNewJob(NewJob src)
        {
            return new JobModel(
                jobTitle: src.JobTitle!,
                jobDescription: src.JobDescription!,
                salaryMin: src.SalaryMin,
                salaryMax: src.SalaryMax,
                currency: src.Currency!,
                location: src.Location!,
                employmentType: src.EmploymentType!,
                requiredSkillName: src.RequiredSkillName!,
                seniorityLevel: src.SeniorityLevel!)
            {
                RequiredSkillLevel = src.RequiredSkillLevel
            };
        }
    }
}
