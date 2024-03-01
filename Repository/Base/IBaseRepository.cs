using Joboard.Entities;

namespace Joboard.Repository.Base
{
    public interface IBaseRepository<T> where T : Activity
    {
        Task<List<T>> GetAllCompaniesAsync();
        Task<T> GetCompanyByIdAsync(int? id);
        Task<bool> CreateCompanyAsync(T entity);
        Task<bool> UpdateCompanyAsync(T entity);
        Task<bool> DeleteCompanyAsync(int? id);
    }
}
