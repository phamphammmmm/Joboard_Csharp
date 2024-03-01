using Joboard.Context;
using Joboard.Repository.Job;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Repository.Job
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<bool> CreateJobAsync(Entities.Job job)
        {
            try
            {
                _context.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateJobAsync(Entities.Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJobAsync(int? jobId)
        {
            if (jobId == null) throw new ArgumentNullException(nameof(jobId), "JobId is required");

            var job = await _context.Jobs.FirstOrDefaultAsync(r => r.Id == jobId);
            if (job == null) return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Entities.Job> GetJobByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Job ID is required");

            var job = await _context.Jobs.FirstOrDefaultAsync(r => r.Id == id);
            if (job == null)
            {
                throw new KeyNotFoundException($"Job with ID {id} not found");
            }

            return job;
        }
    }
}
