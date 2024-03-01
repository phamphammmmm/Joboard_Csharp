using Joboard.Context;
using Joboard.DTO.Company;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Repository.Company
{
    public class CompanyRepositoy : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepositoy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCompanyAsync(Entities.Company company)
        {
            try
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCompanyAsync(int? id)
        {
            if(id == null) throw new ArgumentNullException("id");
            var company = _context.Companies.FirstOrDefault(x => x.Id == id);
            if(company != null)
            {

                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<List<Entities.Company>> GetAllCompaniesAsync()
        {
            List<Entities.Company> companies = await _context.Companies.ToListAsync();
            return companies;
        }

        public async Task<Entities.Company> GetCompanyByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException("Id is not found!!");

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
            {
                throw new ArgumentNullException("Company is not found !!");
            }

            return company;
        }


        public async Task<bool> UpdateCompanyAsync(Entities.Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
