using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.IdentityDTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
