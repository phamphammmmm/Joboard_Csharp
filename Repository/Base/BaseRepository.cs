
using Joboard.Entities;

namespace Joboard.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Activity
    {
        public Task<bool> CreateCompanyAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCompanyAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllCompaniesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetCompanyByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCompanyAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
