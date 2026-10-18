using E_commerceFinalAPI.DTO.Auth;
using E_commerceFinalAPI.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace E_commerceFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        public AuthController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if(dto==null||!ModelState.IsValid) { return BadRequest(); }

            var token = await _unit.authRepo.Login(dto);
            if(token==null) { return Unauthorized("email or password does not exeist"); }

            return Ok(token);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (dto == null || !ModelState.IsValid) { return BadRequest(); }

            var u=await _unit.authRepo.Register(dto);
            if (!u) { return BadRequest("Email already exists"); }

            return Ok("Created Successfuly!!!!");

        }
    }
}
