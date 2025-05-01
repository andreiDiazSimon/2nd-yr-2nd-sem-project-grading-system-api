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
            Console.WriteLine($"username: {username}\npassword: {password}");

            string messageSuccess = "Sign In successful";

            if (username == "admin" && password == "admin")
                return new SignInResponseDto { Success = true, Role = "admin", Message = messageSuccess };

            var studentUser = await _context.Students
                .FirstOrDefaultAsync(s => s.Username == username && s.Password == password);

            var teacherUser = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Username == username && t.Password == password);

            if (studentUser != null)
            {
                return new SignInResponseDto
                {
                    Id = studentUser.Id,
                    Success = true,
                    Role = "student",
                    Message = messageSuccess,
                    Username = studentUser.Username
                };
            }
            if (teacherUser != null)
            {
                return new SignInResponseDto
                {
                    Id = teacherUser.Id,
                    Success = true,
                    Role = "teacher",
                    Message = messageSuccess,
                    Username = teacherUser.Username
                };
            }

            return new SignInResponseDto { Success = false, Message = "Invalid credentials" };
        }
    }
}
