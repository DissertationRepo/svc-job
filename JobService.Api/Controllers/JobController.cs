using AutoMapper;
using System;
using AutoMapper;
using JobService.Api.Models;
using JobService.Application.Abstract_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IJobRequirementService _jobRequirementService;
        private readonly IJobBenefitService _jobBenefitService;
        private readonly IMapper _mapper;

        public JobController(
            IJobService jobService,
            IJobRequirementService jobRequirementService,
            IJobBenefitService jobBenefitService,
            IMapper mapper)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _jobRequirementService = jobRequirementService ?? throw new ArgumentNullException(nameof(jobRequirementService));
            _jobBenefitService = jobBenefitService ?? throw new ArgumentNullException(nameof(jobBenefitService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ----- Job (aggregate) -----

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateJob([FromBody] NewJob newJob)
        {
            // extract user id from token
            var userIdClaim = User.FindFirst("uid");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var claimUserId))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            var jobCommand = _mapper.Map<JobService.Application.Models.JobModel>(newJob);
            await _jobService.CreateJobAsync(jobCommand, claimUserId);
            return Ok();
        }

        [HttpGet("job/{id}")]
        [Authorize]
        public async Task<IActionResult> GetJobById(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Invalid job ID format. Please provide a valid GUID.");
            }

            var domainJob = await _jobService.GetJobByIdAsync(guid);
            if (domainJob == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<JobResponse>(domainJob);
            return Ok(response);
        }

        // GET /jobs/search?q=term
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchJobs([FromQuery(Name = "q")] string? q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Query parameter 'q' is required.");
            }

            var domainJobs = await _jobService.SearchJobsAsync(q);
            var response = _mapper.Map<ICollection<JobResponse>>(domainJobs);
            return Ok(response);
        }

        // GET /jobs/me - returns jobs for the authenticated user (reads uid claim from token)
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyJobs()
        {
            // The access token contains the user id in the "uid" claim.
            var userIdClaim = User.FindFirst("uid");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var claimUserId))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            var domainJobs = await _jobService.GetJobsByUserAsync(claimUserId);
            var response = _mapper.Map<ICollection<JobResponse>>(domainJobs);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteJob(string id)
        {
            if (!Guid.TryParse(id, out var jobId))
            {
                return BadRequest("Invalid job ID format. Please provide a valid GUID.");
            }

            var userIdClaim = User.FindFirst("uid");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var claimUserId))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            var deleted = await _jobService.DeleteJobAsync(jobId, claimUserId);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

        // ----- Job Requirements -----

        [HttpPost("requirement/add")]
        [Authorize]
        public async Task<IActionResult> AddJobRequirement([FromBody] AddJobRequirement addJobRequirement)
        {
            var model = _mapper.Map<Application.Models.JobRequirementModel>(addJobRequirement);
            await _jobRequirementService.AddJobRequirementAsync(model);
            return Ok();
        }

        [HttpGet("requirements/{jobId}")]
        [Authorize]
        public async Task<ICollection<RequirementsResponse>> GetJobRequirements(string jobId)
        {
            var domain = await _jobRequirementService.GetJobRequirementsAsync(jobId);
            return _mapper.Map<ICollection<RequirementsResponse>>(domain);
        }

        [HttpPut("requirement/update")]
        [Authorize]
        public async Task<IActionResult> UpdateJobRequirement([FromBody] UpdateJobRequirement updateJobRequirement)
        {
            var model = _mapper.Map<Application.Models.UpdateJobRequirementModel>(updateJobRequirement);
            var updated = await _jobRequirementService.UpdateJobRequirementAsync(model);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("requirement/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteJobRequirement(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return BadRequest("Invalid requirement ID format. Please provide a valid GUID.");
            }

            var deleted = await _jobRequirementService.DeleteJobRequirementAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        // ----- Job Benefits -----

        [HttpPost("benefit/add")]
        [Authorize]
        public async Task<IActionResult> AddJobBenefit([FromBody] AddJobBenefit addJobBenefit)
        {
            var model = _mapper.Map<Application.Models.JobBenefitModel>(addJobBenefit);
            await _jobBenefitService.AddJobBenefitAsync(model);
            return Ok();
        }

        [HttpGet("benefits/{jobId}")]
        [Authorize]
        public async Task<ICollection<BenefitsResponse>> GetJobBenefits(string jobId)
        {
            var domain = await _jobBenefitService.GetJobBenefitsAsync(jobId);
            return _mapper.Map<ICollection<BenefitsResponse>>(domain);
        }

        [HttpPut("benefit/update")]
        [Authorize]
        public async Task<IActionResult> UpdateJobBenefit([FromBody] UpdateJobBenefit updateJobBenefit)
        {
            var model = _mapper.Map<Application.Models.UpdateJobBenefitModel>(updateJobBenefit);
            var updated = await _jobBenefitService.UpdateJobBenefitAsync(model);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("benefit/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteJobBenefit(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return BadRequest("Invalid benefit ID format. Please provide a valid GUID.");
            }

            var deleted = await _jobBenefitService.DeleteJobBenefitAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
