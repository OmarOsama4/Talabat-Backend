using Shared.DataTransferObjects.IdentityDTO;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
    }
}
