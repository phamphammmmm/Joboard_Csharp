using Joboard.DTO.User;

namespace Joboard.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Authenticate(UserLogin_DTO userLogin_DTO);
    }

    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; } = null!;
    }
}
