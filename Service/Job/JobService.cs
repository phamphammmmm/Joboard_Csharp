using DocumentFormat.OpenXml.Spreadsheet;
using Joboard.DTO.Job;
using Joboard.DTO.User;
using Joboard.Repository.Job;
using Joboard.Service.Job;
using Joboard.Service.Tag;

namespace Joboard.Service.Job
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<bool> CreateJobAsync(JobCreate_DTO jobCreate_DTO)
        {
            if (jobCreate_DTO != null)
            {
                var newJob = new Entities.Job(jobCreate_DTO.Title,
                                              jobCreate_DTO.StartTime,
                                              jobCreate_DTO.EndTime)
                {
                    UserId = jobCreate_DTO.UserId,
                    CategoryId = jobCreate_DTO.CategoryId,
                    CompanyId = jobCreate_DTO.CompanyId,
                    Title = jobCreate_DTO.Title,
                    Description = jobCreate_DTO.Description,
                    IsPremium = jobCreate_DTO.IsPremium,
                    isActive = jobCreate_DTO.isActive,
                    Create_at = jobCreate_DTO.Create_at,
                    Location = jobCreate_DTO.Location,
                    Salary = jobCreate_DTO.Salary,
                    Deadline = jobCreate_DTO.Deadline,
                    TagIds = jobCreate_DTO.TagIds,
                    Requirements = jobCreate_DTO.Requirements,
                    Level = jobCreate_DTO.Level,
                    EXP = jobCreate_DTO.EXP,
                    isRemote = jobCreate_DTO.isRemote,
                    NumberOfVacancies = jobCreate_DTO.NumberOfVacancies,
                    Type = jobCreate_DTO.Type,

                };

                await _jobRepository.CreateJobAsync(newJob);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteJobAsync(int? id)
        {
            await _jobRepository.DeleteJobAsync(id);
            return true;
        }

        public async Task<List<Entities.Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        public Task<Entities.Job> GetJobByIdAsync(int? id)
        {
            return _jobRepository.GetJobByIdAsync(id);
        }

        public async Task<bool> UpdateJobAsync(int? id, JobEdit_DTO jobEdit_DTO)
        {
            if (id == null)
            {
                return false;
            }

            var jobDB = await _jobRepository.GetJobByIdAsync(id);
            if (jobDB == null)
            {
                return false;
            }

            jobDB.Update_at = jobEdit_DTO.Updated_at;
            jobDB.Title = jobEdit_DTO.Title;
            jobDB.Location = jobEdit_DTO.Location;
            jobDB.Type = jobEdit_DTO.Type;
            jobDB.Salary = jobEdit_DTO.Salary;
            jobDB.Deadline = jobEdit_DTO.Deadline;
            jobDB.CategoryId = jobEdit_DTO.CategoryId;
            jobDB.CompanyId = jobEdit_DTO.CompanyId;
            jobDB.isActive = jobEdit_DTO.isActive;
            jobDB.Description = jobEdit_DTO.Description;
            jobDB.EXP = jobEdit_DTO.EXP;
            jobDB.TagIds = jobEdit_DTO.TagIds;
            jobDB.Level = jobEdit_DTO.Level;
            jobDB.isRemote = jobEdit_DTO.isRemote;
            jobDB.Requirements = jobEdit_DTO.Requirements;
            jobDB.NumberOfVacancies = jobEdit_DTO.NumberOfVacancies;

            await _jobRepository.UpdateJobAsync(jobDB);
            return true;
        }
    }
}
