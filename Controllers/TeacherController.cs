using GradingSystemApi.Models;
using GradingSystemApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;  // Inject AppDbContext directly

        public TeacherController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;  // Assign AppDbContext to the field
        }

        // GET method to fetch all sections
        [HttpGet("get-all-sections")]
        public async Task<IActionResult> GetSections()
        {
            // Querying for all distinct sections from the Student model
            var sections = await _appDbContext.Students
                .Select(s => s.Section)
                .Distinct()
                .ToListAsync();

            // Return 404 if no sections are found
            if (sections == null || !sections.Any())
            {
                return NotFound(new { message = "No sections found." });
            }

            // Return the list of sections
            return Ok(sections);
        }

/* --------------------- */

[HttpGet("grades/section/{section}/teacher/{teacherId}")]
public async Task<IActionResult> GetStudentsAndGradesBySectionAndTeacher(string section, int teacherId)
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

    return Ok(studentsWithGrades);
}

/* ---------------------------------- */



[HttpPut("grades/update")]
public async Task<IActionResult> UpdateGrade([FromBody] StudentGradeUpdateModel updatedGrade)
{
    var existingGrade = await _appDbContext.Grades.FirstOrDefaultAsync(g =>
        g.StudentId == updatedGrade.StudentId &&
        g.TeacherId == updatedGrade.TeacherId &&
        g.Term == updatedGrade.Term);

    if (existingGrade != null)
    {
        // Update existing grade
        existingGrade.Week1 = updatedGrade.Week1;
        existingGrade.Week2 = updatedGrade.Week2;
        existingGrade.Week3 = updatedGrade.Week3;
        existingGrade.Week4 = updatedGrade.Week4;
        existingGrade.Week5 = updatedGrade.Week5;
        existingGrade.Exam = updatedGrade.Exam;

        try
        {
            await _appDbContext.SaveChangesAsync();
            return Ok(new { message = "Grade updated successfully." });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { message = "Error saving grade.", details = ex.InnerException?.Message ?? ex.Message });
        }
    }
    else
    {
        return NotFound(new { message = "Grade record not found." });
    }
}



    }
}
