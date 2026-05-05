using JobService.Application.Abstract_Services;
using JobService.Application.Services;
using JobService.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobService, Application.Services.JobService>();
            services.AddScoped<IJobRequirementService, JobRequirementService>();
            services.AddScoped<IJobRequirementRepository, JobRequirementRepository>();
            services.AddScoped<IJobBenefitService, JobBenefitService>();
            services.AddScoped<IJobBenefitRepository, JobBenefitRepository>();
            return services;
        }
    }
}
