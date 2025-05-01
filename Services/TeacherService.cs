using GradingSystemApi.Data;
using GradingSystemApi.Dtos;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystemApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _appDbContext;

        public TeacherService(AppDbContext ctx)
        {
            _appDbContext = ctx;
        }

        public async Task<List<string>> GetSectionsAsync()
        {
            var sections = await _appDbContext.Students
                .Select(s => s.Section)
                .Distinct()
                .ToListAsync();

            return sections ?? new List<string>(); 
        }


        public async Task<dynamic> GetStudentsAndGradesBySectionAndTeacherAsync(string section, int teacherId)
        {

            var studentsWithGrades = await _appDbContext.Students
                .Where(s => s.Section == section)
                .Select(s => new
                {
                    s.Id,
                    s.Username,
                    Grades = s.Grades.Where(g => g.TeacherId == teacherId)
                        .Select(g => new
                        {
                            g.Term,
                            g.Week1,
                            g.Week2,
                            g.Week3,
                            g.Week4,
                            g.Week5,
                            g.Exam
                        })
                        .ToList()
                })
            .ToListAsync();
            return studentsWithGrades;
        }



        public async Task<bool> UpdateGradeAsync(StudentGradeUpdateModel updatedGrade)
        {
            var existingGrade = await _appDbContext.Grades.FirstOrDefaultAsync(g =>
                g.StudentId == updatedGrade.StudentId &&
                g.TeacherId == updatedGrade.TeacherId &&
                g.Term == updatedGrade.Term);

            if (existingGrade != null)
            {
                existingGrade.Week1 = updatedGrade.Week1;
                existingGrade.Week2 = updatedGrade.Week2;
                existingGrade.Week3 = updatedGrade.Week3;
                existingGrade.Week4 = updatedGrade.Week4;
                existingGrade.Week5 = updatedGrade.Week5;
                existingGrade.Exam = updatedGrade.Exam;

                try
                {
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false; 
                }
            }
            return false; 
        }
    }
}
