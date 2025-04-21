using GradingSystemApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/admin/teacher")]
    public class AdminTeacherController : ControllerBase
    {
        private readonly IAdminTeacherService _adminTeacherService;

        public AdminTeacherController(IAdminTeacherService adminTeacherService)
        {
            _adminTeacherService = adminTeacherService;
        }

        [HttpGet("get-all-teacher")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _adminTeacherService.GetAllTeachers();
            return Ok(teachers);
        }
    }
}
