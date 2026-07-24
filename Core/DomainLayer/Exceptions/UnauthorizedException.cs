namespace DomainLayer.Exceptions
{
    public sealed class UnauthorizedException(string message = "Invalid email or password") : Exception(message)
    {
    }
}
