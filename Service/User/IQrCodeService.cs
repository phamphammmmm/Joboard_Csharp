using Joboard.Entities.Customer;

namespace Joboard.Service.User
{
    public interface IQrCodeService
    {
        string GenerateQRCodeUrl(Entities.Customer.User user);
        void DeleteQrCodeImage(int id);
    }
}
