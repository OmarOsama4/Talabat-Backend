using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.IdentityDTO
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string PhoneNumber { get; set; } = default!;
    }
}
