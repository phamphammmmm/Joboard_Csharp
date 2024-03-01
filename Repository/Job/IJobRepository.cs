namespace Joboard.Repository.Job
{
    public interface IJobRepository
    {
        Task<List<Entities.Job>> GetAllJobsAsync();
        Task<Entities.Job> GetJobByIdAsync(int? id);
        Task<bool> CreateJobAsync(Entities.Job job);
        Task<bool> UpdateJobAsync(Entities.Job job);
        Task<bool> DeleteJobAsync(int? id);
    }
}
