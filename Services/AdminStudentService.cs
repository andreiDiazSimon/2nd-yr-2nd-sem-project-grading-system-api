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
    Console.WriteLine($"username: {username}\npassword: {password}\nsection: {section}");

    // Create the new student
    var newStudent = new Student
    {
        Username = username,
        Password = password,
        Section = section
    };

    _context.Students.Add(newStudent);
    await _context.SaveChangesAsync(); // Save the new student to get the Id

    // Automatically associate teachers with the new student
    var teachers = await _context.Teachers.ToListAsync();  // Fetch all teachers (you can modify this if you need to filter specific teachers)

    if (teachers == null || !teachers.Any())
    {
        return new AdminAddStudentResponseDto
        {
            Success = false,
            Message = "Cannot add student: No teachers found in the system."
        };
    }

    foreach (var teacher in teachers)
    {
        var studentTeacher = new StudentTeacher
        {
            StudentId = newStudent.Id,
            TeacherId = teacher.Id
        };

        _context.StudentTeachers.Add(studentTeacher); // Add the association to the join table
    }

    await _context.SaveChangesAsync(); // Save the student-teacher associations

    // Add default grades per teacher and term
    var terms = new[] { "Prelim", "Midterm", "Finals" };
    foreach (var teacher in teachers)
    {
        foreach (var term in terms)
        {
            var grade = new Grade
            {
                StudentId = newStudent.Id,
                TeacherId = teacher.Id,  // Ensure the grade is assigned to the specific teacher
                Term = term,
                Week1 = 0.0,
                Week2 = 0.0,
                Week3 = 0.0,
                Week4 = 0.0,
                Week5 = 0.0,
                Exam = 0.0
            };

            _context.Grades.Add(grade); // Add the grade record for the teacher and term
        }
    }

    await _context.SaveChangesAsync(); // Save the grades

    return new AdminAddStudentResponseDto
    {
        Success = true,
        Message = "Student added successfully with teachers and grades."
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
