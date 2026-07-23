namespace DomainLayer.Exceptions
{
    public sealed class BadRequestException(List<string> errors) : Exception("Validation Errors Occurred")
    {
        public List<string> Errors { get; } = errors;
    }
}
