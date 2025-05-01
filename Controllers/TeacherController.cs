using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Models;
using GradingSystemApi.Dtos;
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
        private readonly ITeacherService _teacherService;
        private readonly AppDbContext _appDbContext;

        public TeacherController(ITeacherService teacherService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _teacherService = teacherService;
        }

        /* ---------------------------------- */


        [HttpGet("get-all-sections")]
        public async Task<IActionResult> GetSections()
        {
            var sections = await _teacherService.GetSectionsAsync();
            if (sections == null || sections.Count == 0)
            {
                return NotFound(new { message = "No sections found." });
            }
            return Ok(sections);
        }

        /* ---------------------------------- */

        [HttpGet("grades/section/{section}/teacher/{teacherId}")]
        public async Task<IActionResult> GetStudentsAndGradesBySectionAndTeacher(string section, int teacherId)
        {
            var studentsWithGrades = await _teacherService.GetStudentsAndGradesBySectionAndTeacherAsync(section, teacherId);
            return Ok(studentsWithGrades);
        }

        /* ---------------------------------- */

        [HttpPut("grades/update")]
        public async Task<IActionResult> UpdateGrade([FromBody] StudentGradeUpdateModel updatedGrade)
        {
            var success = await _teacherService.UpdateGradeAsync(updatedGrade);
            if (success)
            {
                return Ok(new { message = "Grade updated successfully." });
            }
            else
            {
                return NotFound(new { message = "Grade record not found." });
            }
        }
    }
}
