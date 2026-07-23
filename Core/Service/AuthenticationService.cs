using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTO;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var User = await userManager.FindByEmailAsync(loginDTO.Email) ?? throw new UserNotFoundException(loginDTO.Email);
            var isPasswordValid = await userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (isPasswordValid)
                return new UserDTO
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email!,
                    Token = "ToDo"
                };
            else
                throw new UnauthorizedException();
        }

        public Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
