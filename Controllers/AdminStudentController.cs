using GradingSystemApi.Dtos;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Data;
using GradingSystemApi.Models;
using GradingSystemApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/admin/student")]
    public class AdminStudentController : ControllerBase
    {


        private readonly IAdminStudentService _adminStudentService;

        public AdminStudentController(IAdminStudentService adminStudentService)
        {
            _adminStudentService = adminStudentService;
        }

        [HttpPost("add-student")]
        public async Task<IActionResult> AdminAddStudent([FromBody] AdminAddStudentRequestDto request)
        {
            var result = await _adminStudentService.AdminAddStudent(request.username, request.password, request.section);
            return Ok(result);
        }

        [HttpGet("get-all-student")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _adminStudentService.GetAllStudents();
            return Ok(students);
        }



        [HttpPost("admin-remove-student")]
        public async Task<IActionResult> AdminRemoveStudent([FromBody] AdminRemoveStudentRequestDto request)
        {
            var success = await _adminStudentService.RemoveStudent(request.id);
            if (!success)
            {
                return NotFound(new { message = "Student not found." });
            }

            return Ok(new { message = "Student removed successfully." });
        }

    }
}
