using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var user = await serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(user);
        }

    }
}
