using GradingSystemApi.Dtos;
using GradingSystemApi.Models;

namespace GradingSystemApi.Interfaces
{
    public interface ISignInService
    {
        Task<SignInResponseDto> SignIn(string username, string password);
    }
}
