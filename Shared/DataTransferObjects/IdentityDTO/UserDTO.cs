namespace Shared.DataTransferObjects.IdentityDTO
{
    public class UserDTO
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
