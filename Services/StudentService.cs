using GradingSystemApi.Data;
using GradingSystemApi.Dtos;
using GradingSystemApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystemApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentGradeDto>> GetCombinedGrades(int studentId)
        {
            var grades = await _context.Grades
                .Include(g => g.Teacher)
                .Where(g => g.StudentId == studentId)
                .ToListAsync();

            var grouped = grades
                .GroupBy(g => new { g.TeacherId, g.Teacher.Username, g.Teacher.Subject })
                .Select(g => new StudentGradeDto
                {
                    TeacherName = g.Key.Username,
                    Subject = g.Key.Subject,
                    CombinedPrelimGrade = g.FirstOrDefault(x => x.Term.ToLower() == "prelim") != null
                        ? RoundToTwoDecimalPlaces(AverageGrade(g.First(x => x.Term.ToLower() == "prelim")))
                        : null,
                    CombinedMidtermGrade = g.FirstOrDefault(x => x.Term.ToLower() == "midterm") != null
                        ? RoundToTwoDecimalPlaces(AverageGrade(g.First(x => x.Term.ToLower() == "midterm")))
                        : null,
                    CombinedFinalsGrade = g.FirstOrDefault(x => x.Term.ToLower() == "finals") != null
                        ? RoundToTwoDecimalPlaces(AverageGrade(g.First(x => x.Term.ToLower() == "finals")))
                        : null
                })
                .ToList();

            return grouped;
        }

        private double AverageGrade(Models.Grade g)
        {
            return (g.Week1 + g.Week2 + g.Week3 + g.Week4 + g.Week5 + g.Exam) / 6;
        }

        private double RoundToTwoDecimalPlaces(double value)
        {
            return Math.Round(value * 100) / 100;
        }
    }
}
