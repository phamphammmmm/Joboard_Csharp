using Joboard.DTO.Company;

namespace Joboard.Repository.Company
{
    public interface ICompanyRepository 
    {
        Task<List<Entities.Company>> GetAllCompaniesAsync();
        Task<Entities.Company> GetCompanyByIdAsync(int? id);
        Task<bool> CreateCompanyAsync(Entities.Company company);
        Task<bool> UpdateCompanyAsync(Entities.Company company);
        Task<bool> DeleteCompanyAsync(int? id);
    }
}

