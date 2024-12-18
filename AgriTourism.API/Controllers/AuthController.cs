using AgriTourism.DTO;
using AgriTourism.Handler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriTourism.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody]LoginDTO loginDTO)
        {
            var token = await _authService.Login(loginDTO);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
        [HttpPost("AddReviewer")]
        public async Task<IActionResult> addReviewer([FromBody]ReviewerDTO reviewerDTO)
        {
            if(reviewerDTO == null)
            {
                return BadRequest("reviewer data is null");
            }
            var reviewer= await _authService.AddReviewer(reviewerDTO);
            if(reviewer>0)
            {
                return Ok("User added successfully");
            }
            else
            {
                return StatusCode(500, "an error occured when adding the user");
            }

        }

    }
}
