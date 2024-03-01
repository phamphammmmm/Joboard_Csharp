using Joboard.DTO.Job;
using Joboard.Service.Job;
using Microsoft.AspNetCore.Mvc;

namespace Joboard.Controllers
{
    [Route("/api/job")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly ElasticsearchControllers _elasticsearchControllers;
        private readonly IJobService _jobService;

        public JobController(ElasticsearchControllers elasticsearchControllers, IJobService jobService)
        {
            _elasticsearchControllers = elasticsearchControllers;
            _jobService = jobService;
        }

        [HttpPost("stop-sync-job")]
        public IActionResult StopSyncJob()
        {
            _elasticsearchControllers.StopSyncJob();
            return Ok("Sync job stopped");
        }

        public async Task<IActionResult> GetAllJobs()
        {
            var companies = await _jobService.GetAllJobsAsync();
            return Ok(companies);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetJobById(int? id)
        {
            var Job = await _jobService.GetJobByIdAsync(id);
            return Ok(Job);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJob(JobCreate_DTO jobCreate_DTO)
        {
            var result = await _jobService.CreateJobAsync(jobCreate_DTO);
            if (result)
            {
                return Ok(new { Message = "Job created successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to create Job" });
            }
        }


        [HttpPost("update/{JobId}")]
        public async Task<IActionResult> UpdateJob(int? JobId, JobEdit_DTO JobEdit_DTO)
        {
            var result = await _jobService.UpdateJobAsync(JobId, JobEdit_DTO);
            if (result)
            {
                return Ok(new { Message = "Job updated successfully" });
            }
            else
            {
                return NotFound(new { Message = "Job not found" });
            }
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteJob(int? Id)
        {
            var result = await _jobService.DeleteJobAsync(Id);
            if (result)
            {
                return Ok(new { Message = "Job deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Job not found" });
            }
        }
    }
}
