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
    [Route("api")]
    public class SignInController : ControllerBase
    {
        private readonly ISignInService _signInService;

        public SignInController(ISignInService signInService)
        {
            _signInService = signInService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<List<AdminGetAllStudentResponseDto>>> SignIn([FromBody] SignInRequestDto request)
        {
            Console.WriteLine(request.username);
            Console.WriteLine(request.password);
            var task = await _signInService.SignIn(request.username, request.password);

            if (task == null)
                return Unauthorized(task);
            return Ok(task);
        }
    }
}
