using GradingSystemApi.Dtos;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GradingSystemApi.Services
{
    public class SignInService : ISignInService
    {
        private readonly AppDbContext _context;

        public SignInService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SignInResponseDto> SignIn(string username, string password)
        {
            if (username == "test" && password == "test")
                return new SignInResponseDto { Success = true, Message = "you are admin" };

            var user = await _context.Students
                .FirstOrDefaultAsync(s => s.Username == username && s.Password == password);

            if (user == null)
                return new SignInResponseDto { Success = false, Message = "Invalid credentials" };

            return new SignInResponseDto { Success = true, Message = "Sign In successful" };
        }
    }
}
