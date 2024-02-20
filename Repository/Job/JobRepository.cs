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

        //public async Task<List<Entities.Job>> GetAllJobsAsync()
        //{
        //    //return await _context.Jobs.ToListAsync();
        //}

        public async Task<bool> CreateJobAsync(Entities.Job category)
        {
            try
            {
                //_context.Jobs.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateJobAsync(Entities.Job category)
        {
            //_context.Jobs.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJobAsync(int? categoryId)
        {
            if (categoryId == null)
            {
                throw new ArgumentNullException(nameof(categoryId), "JobId is required");
            }

            //var category = await _context.Jobs.FirstOrDefaultAsync(r => r.Id == categoryId);
            //if (category == null)
            //{
            //    return false;
            //}

            //_context.Jobs.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<Entities.Job> GetJobByIdAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        throw new ArgumentNullException(nameof(id), "Job ID is required");
        //    }

            //var category = await _context.Jobs.FirstOrDefaultAsync(r => r.Id == id);
            //if (category == null)
            //{
            //    throw new KeyNotFoundException($"Job with ID {id} not found");
            //}

            //return category;
        //}
    }
}
