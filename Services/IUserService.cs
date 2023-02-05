using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterModel model);
        Task<JwtToken> LoginUser(LoginModel model);
        Task<UserDTO> GetUserFromToken(HttpRequest request);
    }
}
