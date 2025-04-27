using GradingSystemApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradingSystemApi.Controllers
{
    [ApiController]
    [Route("api/admin/section")]
    public class AdminSectionController : ControllerBase
    {
        private readonly IAdminSectionService _adminSectionService;

        public AdminSectionController(IAdminSectionService adminSectionService)
        {
            _adminSectionService = adminSectionService;
        }



        [HttpGet("get-all-section")]
        public async Task<IActionResult> GetAllSections()
        {
            var sections = await _adminSectionService.GetAllSections();
            return Ok(sections);
        }



        [HttpGet("get-students-by-section/{section}")]
        public async Task<IActionResult> GetStudentsBySection(string section)
        {
            var students = await _adminSectionService.GetStudentsBySection(section);
            return Ok(students);
        }
    }
}
