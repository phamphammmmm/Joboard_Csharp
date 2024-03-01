using Joboard.DTO.Company;
using Joboard.DTO.User;
using Joboard.Repository.Company;
using Joboard.Service.Tag;

namespace Joboard.Service.Company
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;
        private readonly IImageService _imageService;

        public CompanyService(ICompanyRepository companyRepository, IImageService imageService)
        {
            _companyRepository = companyRepository;
            _imageService = imageService;
        }

        public async Task<bool> CreateCompanyAsync(CompanyCreate_DTO companyCreate_DTO)
        {
            if (companyCreate_DTO.Avatar_Image_File == null || companyCreate_DTO.Cover_Image_File == null)
            {
                return false;
            }

            string Avatar_Image_Path = await _imageService.SaveImageAsync(file: companyCreate_DTO.Avatar_Image_File, folderName: "company");
            string Cover_Image_Path = await _imageService.SaveImageAsync(file: companyCreate_DTO.Cover_Image_File, folderName: "company");

            if (companyCreate_DTO != null)
            {
                var newCompany = new Entities.Company
                {
                    Name = companyCreate_DTO.Name,
                    Email = companyCreate_DTO.Email,
                    Scale = companyCreate_DTO.Scale,
                    Address = companyCreate_DTO.Address,
                    Create_at = companyCreate_DTO.Created_at,
                    Avatar_Img_Path = Avatar_Image_Path,
                    Cover_Img_Path = Cover_Image_Path,
                };

                await _companyRepository.CreateCompanyAsync(newCompany);
                return true;
            }
            else { 
                return false; 
            }
        }

        public async Task<bool> DeleteCompanyAsync(int? id)
        {
            await _companyRepository.DeleteCompanyAsync(id);
            return true;
        }

        public async Task<List<Entities.Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllCompaniesAsync();
        }

        public Task<Entities.Company> GetCompanyByIdAsync(int? id)
        {
            return _companyRepository.GetCompanyByIdAsync(id);
        }

        public async Task<bool> UpdateCompanyAsync(int? id, CompanyEdit_DTO companyEdit_DTO)
        {
            if (id == null)
            {
                return false;
            }

            var companyDB = await _companyRepository.GetCompanyByIdAsync(id);
            if (companyDB == null)
            {
                return false;
            }

            string Avatar_Image_Path = null; 
            string Cover_Image_Path = null; 

            if (companyEdit_DTO.Avatar_Image_File != null)
            {
                if (!string.IsNullOrEmpty(companyDB.Avatar_Img_Path))
                {
                    _imageService.DeleteImageAsync(companyDB.Avatar_Img_Path);
                }

                Avatar_Image_Path = await _imageService.SaveImageAsync(file: companyEdit_DTO.Avatar_Image_File, folderName: "company");
            }

            if (companyEdit_DTO.Cover_Image_File != null)
            {
                if (!string.IsNullOrEmpty(companyDB.Cover_Img_Path))
                {
                    _imageService.DeleteImageAsync(companyDB.Cover_Img_Path);
                }
                Cover_Image_Path = await _imageService.SaveImageAsync(file: companyEdit_DTO.Cover_Image_File, folderName: "company");
            }

            companyDB.Update_at = companyEdit_DTO.Updated_at;
            companyDB.Name = companyEdit_DTO.Name;
            companyDB.Address = companyEdit_DTO.Address;
            companyDB.Email = companyEdit_DTO.Email;

            if(Avatar_Image_Path != null) companyDB.Avatar_Img_Path = Avatar_Image_Path;
            if (Cover_Image_Path != null) companyDB.Cover_Img_Path = Cover_Image_Path;

            await _companyRepository.UpdateCompanyAsync(companyDB);
            return true;
        }
    }
}
