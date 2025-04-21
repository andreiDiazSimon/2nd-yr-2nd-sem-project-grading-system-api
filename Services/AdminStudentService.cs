using GradingSystemApi.Models;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;

using Microsoft.EntityFrameworkCore;

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

        public async Task<List<AdminGetAllStudentResponseDto>> GetAllStudents()
        {
            return await _context.Students
                    .Select(student => new AdminGetAllStudentResponseDto
                    {
                        id = student.Id,
                        username = student.Username,
                        section = student.Section
                    })
                    .ToListAsync();
        }


        public async Task<bool> RemoveStudent(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.Grades)
                .Include(s => s.StudentTeachers)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return false;

            _context.Grades.RemoveRange(student.Grades);
            _context.RemoveRange(student.StudentTeachers);
            _context.Students.Remove(student);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
