using Joboard.DTO.Company;

namespace Joboard.Service.Company
{
    public interface ICompanyService    
    {
        Task<List<Entities.Company>> GetAllCompaniesAsync();
        Task<Entities.Company> GetCompanyByIdAsync(int? id);
        Task<bool> CreateCompanyAsync(CompanyCreate_DTO companyCreate_DTO);
        Task<bool> UpdateCompanyAsync(int? id, CompanyEdit_DTO companyEdit_DTO);
        Task<bool> DeleteCompanyAsync(int? id);
    }
}
