using GradingSystemApi.Dtos;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GradingSystemApi.Services
{
    public class AdminSectionService : IAdminSectionService
    {
        private readonly AppDbContext _context;

        public AdminSectionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllSections()
        {
            var uniqueSections = await _context.Students
                .Select(s => s.Section)
                .Distinct()
                .ToListAsync();

            return uniqueSections;
        }



        public async Task<List<AdminSectionGetStudentBySectionDto>> GetStudentsBySection(string section)
        {
            var students = await _context.Students
                .Where(s => s.Section == section)
                .Select(s => new AdminSectionGetStudentBySectionDto
                {
                    Id = s.Id,
                    Username = s.Username,
                    Section = s.Section
                })
                .ToListAsync();

            return students;
        }

    }
}
