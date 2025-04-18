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
            Console.WriteLine(request.username);
            Console.WriteLine(request.password);
            Console.WriteLine(request.section);
            var result = await _adminStudentService.AdminAddStudent(request.username, request.password, request.section);
            return Ok(result);
        }
    }
}
