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
                    Token = CreateTokenAsync(User)
                };
            else
                throw new UnauthorizedException();
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber
            };
            var result = await userManager.CreateAsync(User, registerDTO.Password);

            if (result.Succeeded)
                return new UserDTO
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email!,
                    Token = CreateTokenAsync(User)
                };
            else
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private static string CreateTokenAsync(ApplicationUser User)
        {
            return "ToDo";
        }
    }
}
