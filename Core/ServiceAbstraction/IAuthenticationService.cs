using Shared.DataTransferObjects.IdentityDTO;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDTO> GetUserAddressAsync(string email);
        Task<AddressDTO> UpdateUserAddressAsync(string email);
        Task<UserDTO> GetUserAsync(string email);
    }
}
