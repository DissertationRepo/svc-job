using AutoMapper;
using JobService.Domain.Entities.Aggregates;
using JobService.Infrastructure.Entities;

namespace JobService.Infrastructure.Mappings
{
    public class DomainJobMapping : Profile
    {
        public DomainJobMapping()
        {
            CreateMap<Job, JobEntity>()
                .ConvertUsing(src => CreateInfrastructureJob(src));
        }

        private JobEntity CreateInfrastructureJob(Job src)
        {
            return new JobEntity
            {
                Id = src.Id.Value,
                JobTitle = src.JobTitle,
                JobDescription = src.JobDescription,
                SalaryMin = src.SalaryRange.Min,
                SalaryMax = src.SalaryRange.Max,
                Currency = src.SalaryRange.Currency,
                Location = src.Location.Value,
                EmploymentType = src.EmploymentType.Value,
                RequiredSkillName = src.RequiredSkill.Name,
                RequiredSkillLevel = src.RequiredSkill.Level,
                SeniorityLevel = src.SeniorityLevel.Value,
                CreatedAt = src.CreatedAt,
                UpdatedAt = src.UpdatedAt
            };
        }
    }
}
