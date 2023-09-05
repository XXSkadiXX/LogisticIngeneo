using Logistic.Domain.DTO.Logistic.User;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface IUserServices
    {
        TokenDto Login(LoginDto loginDto);
        Task<bool> RegisterUser(AddUserDto register);
    }
}
