using GradingSystemApi.Dtos;

namespace GradingSystemApi.Interfaces
{
    public interface ISignInService
    {
        Task<SignInResponseDto> SignIn(string username, string password);
    }
}
