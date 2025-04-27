using GradingSystemApi.Models;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystemApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _appDbContext;
        public TeacherService(AppDbContext ctx) => _appDbContext = ctx;

        public async Task<List<string>> GetSectionsAsync()
        {
            return await _appDbContext.Students
                .Select(s => s.Section)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<StudentGradeResponseDto>> GetGradesBySectionAsync(string section)
        {
            return await _appDbContext.Students
                .Where(s => s.Section == section)
                .Select(s => new StudentGradeResponseDto
                {
                    Id = s.Id,
                    Username = s.Username,
                    Section = s.Section,
                    Grades = s.Grades.Select(g => new GradeDto
                    {
                        Term = g.Term,
                        Week1 = g.Week1,
                        Week2 = g.Week2,
                        Week3 = g.Week3,
                        Week4 = g.Week4,
                        Week5 = g.Week5,
                        Exam = g.Exam
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<string> SaveGradesAsync(List<SaveGradesDto> gradesDto)
        {
            if (gradesDto == null || !gradesDto.Any())
            {
                return "Invalid grade data.";
            }

            try
            {
                foreach (var dto in gradesDto)
                {
                    var student = await _appDbContext.Students
                        .Include(s => s.Grades)
                        .FirstOrDefaultAsync(s => s.Id == dto.StudentId);

                    if (student == null)
                    {
                        return $"Student with ID {dto.StudentId} not found.";
                    }

                    foreach (var gradeDto in dto.Grades)
                    {
                        var grade = student.Grades
                            .FirstOrDefault(g => g.Term == gradeDto.Term);

                        if (grade == null)
                        {
                            grade = new Grade
                            {
                                StudentId = student.Id,
                                Term = gradeDto.Term
                            };
                            student.Grades.Add(grade);
                        }

                        grade.Week1 = gradeDto.Week1;
                        grade.Week2 = gradeDto.Week2;
                        grade.Week3 = gradeDto.Week3;
                        grade.Week4 = gradeDto.Week4;
                        grade.Week5 = gradeDto.Week5;
                        grade.Exam = gradeDto.Exam;
                    }
                }

                await _appDbContext.SaveChangesAsync();

                return "Grades saved successfully.";
            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex.Message}";
            }
        }
    }
}
