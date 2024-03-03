using Joboard.DTO.Job;
using Joboard.Entities;
using Joboard.Service.Job;
using System.Timers;
using Microsoft.AspNetCore.Mvc;

namespace Joboard.Controllers
{
    [Route("/api/job")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly ElasticsearchControllers _elasticsearchControllers;
        private readonly IJobService _jobService;
        private readonly Queue<Job> jobQueue = new Queue<Job>();

        public JobController(ElasticsearchControllers elasticsearchControllers, IJobService jobService)
        {
            _elasticsearchControllers = elasticsearchControllers;
            _jobService = jobService;

            // Bắt đầu timer để kiểm tra hàng đợi công việc mỗi phút
            var timer = new System.Timers.Timer();
            timer.Interval = 60000; // Mỗi phút
            timer.Elapsed += (sender, e) => CheckJobQueue(sender, e); 
            timer.Start();
        }

        private void CheckJobQueue(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            foreach (Job job in jobQueue.ToArray())
            {
                if (currentTime >= job.StartTime && currentTime <= job.EndTime)
                {
                    Console.WriteLine($"Đăng bài việc: {job.Title}");
                    jobQueue.Dequeue();
                }
            }
        }

        private void AddJobToQueue(string title, DateTime startTime, DateTime endTime)
        {
            if (DateTime.Now < startTime)
            {
                jobQueue.Enqueue(new Job(title, startTime, endTime));
                Console.WriteLine("Công việc đã được thêm vào hàng đợi chờ đăng!");
            }
            else
            {
                Console.WriteLine("Không thể thêm công việc vào hàng đợi vì thời gian bắt đầu đã qua.");
            }
        }

        private void DisplayJobs()
        {
            Console.WriteLine("Danh sách công việc:");
            foreach (Job job in jobQueue)
            {
                Console.WriteLine($"Tiêu đề: {job.Title}, Thời gian bắt đầu: {job.StartTime}, Thời gian kết thúc: {job.EndTime}");
            }
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
                if (jobCreate_DTO.AutoPost)
                {
                    AddJobToQueue(jobCreate_DTO.Title, jobCreate_DTO.StartTime, jobCreate_DTO.EndTime);
                    return Ok(new { Message = "Job created successfully and added to queue for auto-posting" });
                }
                else
                {
                    return Ok(new { Message = "Job created successfully" });
                }
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
