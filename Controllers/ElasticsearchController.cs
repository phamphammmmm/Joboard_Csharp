using Hangfire;
using Joboard.Context;
using Joboard.Entities;
using Joboard.Entities.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Joboard.Controllers
{
    [Route("/api/elastic")]
    [ApiController]
    public class ElasticsearchControllers : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<UserController> _logger;
        private readonly IRecurringJobManager _recurringJobManager;

        public ElasticsearchControllers(ApplicationDbContext context,
                                    ILogger<UserController> logger,
                                    IElasticClient elasticClient,
                                    IRecurringJobManager recurringJobManager)
        {
            _context = context;
            _logger = logger;
            _elasticClient = elasticClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpPost(Name = "Postjobs")]
        public async Task<IActionResult> Post(List<object> documents)
        {
            foreach (var document in documents)
            {
                await _elasticClient.IndexDocumentAsync(document);
            }
            return Ok();
        }

        [HttpGet(Name = "GetJobs")]
        public async Task<IActionResult> Get(string keyword)
        {

            var result = await _elasticClient.SearchAsync<User>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                        )
                    ).Size(1000)
                );
            return Ok(result.Documents.ToList());
        }

        [HttpGet("start-sync-job")]

        public void StartSyncJob()
        {
            _recurringJobManager.AddOrUpdate("ElasticsearchSyncJob", () => SyncDataToElasticsearch(), "*/5 * * * * *");
        }
        [HttpGet("stop-sync-job")]

        public void StopSyncJob()
        {
            _recurringJobManager.RemoveIfExists("ElasticsearchSyncJob");
        }

        [HttpPost("sync-data-to-els")]
        //Sync data from Database to index of ElasticSearch
        public async Task SyncDataToElasticsearch()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Category)
                .Include(j => j.Company)
                .Include(j => j.User)
                .ToListAsync();

            var documents = new List<object>();

            foreach (var job in jobs)
            {
                var tagIds = job.TagIds.Split(',').Select(int.Parse).ToArray();
                var jobTags = await _context.Tags.Where(t => tagIds.Contains(t.Id)).ToListAsync();

                var categoryInfo = new { job.Category.Name };
                var companyInfo = new { job.Company.Name };
                var userInfo = new { job.User.FullName };

                var jobInfo = new
                {
                    job.Id,
                    job.Title,
                    job.Description,
                    job.IsPremium,
                    job.Salary,
                    job.Location,
                    job.isActive,
                    job.Deadline,
                    job.Requirements,
                    job.isRemote,
                    job.NumberOfVacancies,
                    job.Type,
                    User = userInfo,
                    Category = categoryInfo,
                    Company = companyInfo,
                    Tags = jobTags.Select(tag => new { tag.Id, tag.Name }).ToArray()
                };

                documents.Add(jobInfo);
            }

            await Post(documents);
        }
    }
}
