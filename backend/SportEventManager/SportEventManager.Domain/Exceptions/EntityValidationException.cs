namespace SportEventManager.Domain.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
