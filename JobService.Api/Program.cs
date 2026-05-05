using JobService.Api.ModelValidators;
using JobService.Infrastructure;
using JobService.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<NewJobValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddJobRequirementValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddJobBenefitValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(
    typeof(JobService.Api.Mappings.NewJobMapping).Assembly,
    typeof(JobService.Infrastructure.Mappings.DomainJobMapping).Assembly,
    typeof(JobService.Api.Mappings.AddJobRequirementMapping).Assembly,
    typeof(JobService.Infrastructure.Mappings.DomainJobRequirementMapping).Assembly,
    typeof(JobService.Api.Mappings.AddJobBenefitMapping).Assembly,
    typeof(JobService.Infrastructure.Mappings.DomainJobBenefitMapping).Assembly,
    typeof(JobService.Api.Mappings.RequirementsResponseMapping).Assembly,
    typeof(JobService.Api.Mappings.BenefitsResponseMapping).Assembly,
    typeof(JobService.Api.Mappings.DomainJobMapping).Assembly
);

var conString = builder.Configuration.GetConnectionString("JobDB") ??
    throw new InvalidOperationException("Connection string 'JobDB' not found.");
builder.Services.AddDbContext<JobDbContext>(options =>
    options.UseNpgsql(conString));

builder.Services.AddControllers();

// JWT configuration
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

// Swagger with Bearer support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
