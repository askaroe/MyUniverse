using MyUniverse.Dtos;

namespace MyUniverse.Services.Auth
{
    public interface IAuthService<T> where T : class
    {
        Task<T?> Login(LoginDto loginDto);
    }
}
