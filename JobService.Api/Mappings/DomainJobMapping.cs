using AutoMapper;

namespace JobService.Api.Mappings
{
    public class DomainJobMapping : Profile
    {
        public DomainJobMapping()
        {
            CreateMap<Domain.Entities.Aggregates.Job, Models.JobResponse>()
                .ConvertUsing(src => new Models.JobResponse
                {
                    Id = src.Id.Value.ToString(),
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
                });
        }
    }
}
