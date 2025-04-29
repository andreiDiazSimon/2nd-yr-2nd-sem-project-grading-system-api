using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;
using GradingSystemApi.Models;
using GradingSystemApi.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


[HttpGet("{studentId}")]
public async Task<ActionResult<List<StudentGradeDto>>> GetCombinedGrades(int studentId)
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

    return Ok(grouped); // ✅ wrap result in Ok()
}

// ✅ Move these methods to the controller class or service class (but **outside** any method)
private double AverageGrade(Grade g)
{
    return (g.Week1 + g.Week2 + g.Week3 + g.Week4 + g.Week5 + g.Exam) / 6;
}

private double RoundToTwoDecimalPlaces(double value)
{
    return Math.Round(value * 100) / 100;
}

    }
}
