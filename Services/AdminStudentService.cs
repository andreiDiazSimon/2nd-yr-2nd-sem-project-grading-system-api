using GradingSystemApi.Models;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;

namespace GradingSystemApi.Services
{
    public class AdminStudentService : IAdminStudentService
    {
        private readonly AppDbContext _context;

        public AdminStudentService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<AdminAddStudentResponseDto> AdminAddStudent(string username, string password, string section)
        {
            var newStudent = new Student
            {
                Username = username,
                Password = password,
                Section = section,
                Grades = new List<Grade>() 
            };

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return new AdminAddStudentResponseDto
            {
                Success = true,
                Message = "Student added successfully."
            };
        }

    }
}
