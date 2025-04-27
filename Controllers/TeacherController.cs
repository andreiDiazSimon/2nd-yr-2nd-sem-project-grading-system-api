using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _svc;
        public TeacherController(ITeacherService svc) => _svc = svc;

        [HttpGet("sections")]
        public async Task<IActionResult> GetSections()
        {
            return Ok(await _svc.GetSectionsAsync());
        }

        [HttpGet("grades/{section}")]
        public async Task<IActionResult> GetGrades(string section)
        {
            return Ok(await _svc.GetGradesBySectionAsync(section));
        }



        [HttpPost("save-grades")]
        public async Task<IActionResult> SaveGrades([FromBody] List<SaveGradesDto> gradesDto)
        {
            var result = await _svc.SaveGradesAsync(gradesDto);

            if (result == "Grades saved successfully.")
            {
        	return Ok(new { message = "Grades saved successfully." });
            }

            return BadRequest(result);
        }

    }
}
