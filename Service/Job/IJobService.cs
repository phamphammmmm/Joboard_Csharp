using Joboard.DTO.Job;

namespace Joboard.Service.Job
{
    public interface IJobService
    {
        Task<List<Entities.Job>> GetAllJobsAsync();
        Task<Entities.Job> GetJobByIdAsync(int? id);
        Task<bool> CreateJobAsync(JobCreate_DTO jobCreate_DTO);
        Task<bool> UpdateJobAsync(int?id, JobEdit_DTO jobEdit_DTO);
        Task<bool> DeleteJobAsync(int?id);
    }
}
