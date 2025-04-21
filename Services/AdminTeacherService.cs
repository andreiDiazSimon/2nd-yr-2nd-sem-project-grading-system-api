using GradingSystemApi.Dtos;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GradingSystemApi.Services
{
    public class AdminTeacherService : IAdminTeacherService
    {
        private readonly AppDbContext _context;

        public AdminTeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdminGetAllTeacherResponseDto>> GetAllTeachers()
        {
            return await _context.Teachers
                .Select(t => new AdminGetAllTeacherResponseDto
                {
                    id = t.Id,
                    username = t.Username,
                    subject = t.Subject
                })
                .ToListAsync();
        }
    }
}
