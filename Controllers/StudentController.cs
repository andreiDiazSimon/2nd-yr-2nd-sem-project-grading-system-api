using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<List<StudentGradeDto>>> GetCombinedGrades(int studentId)
        {
            var result = await _studentService.GetCombinedGrades(studentId);
            return Ok(result);
        }
    }
}
