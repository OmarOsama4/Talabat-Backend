using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<AddressDTO> GetUserAddressAsync(string email)
        {
            var User = await userManager.Users.Include(U => U.Address)
                                        .FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
                return mapper.Map<Address, AddressDTO>(User.Address);
            else 
                throw new AddressNotFoundException(User.UserName!);
        }

        public async Task<UserDTO> GetUserAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDTO() 
            {
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await CreateTokenAsync(User)
            };
        }

        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var User = await userManager.FindByEmailAsync(loginDTO.Email) ?? throw new UserNotFoundException(loginDTO.Email);
            var isPasswordValid = await userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (isPasswordValid)
                return new UserDTO
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email!,
                    Token = await CreateTokenAsync(User)
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
                    Token = await CreateTokenAsync(User)
                };
            else
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public async Task<AddressDTO> UpdateUserAddressAsync(string email, AddressDTO addressDTO)
        {
            var User = await userManager.Users.Include(U => U.Address)
                                        .FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
            {
                User.Address.FirstName = addressDTO.FirstName;
                User.Address.LastName = addressDTO.LastName;
                User.Address.City = addressDTO.City;
                User.Address.Country = addressDTO.Country;
                User.Address.Street = addressDTO.Street;
            }
            else
            {
                User.Address = mapper.Map<AddressDTO, Address>(addressDTO);
            }
            await userManager.UpdateAsync(User);
            return mapper.Map<AddressDTO>(User.Address);
        }

        private async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>
            {
                new(ClaimTypes.Email, User.Email!),
                new(ClaimTypes.Name, User.UserName!),
                new(ClaimTypes.NameIdentifier, User.Id!)
            };
            var Roles = await userManager.GetRolesAsync(User);
            foreach (var Role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!)); 
            var Creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: Creds
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
