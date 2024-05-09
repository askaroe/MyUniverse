using MyUniverse.Dtos;
using MyUniverse.Models;

namespace MyUniverse.Services.Auth
{
    public class AuthService : IAuthService<UserModel>
    {
        public Task<UserModel?> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
