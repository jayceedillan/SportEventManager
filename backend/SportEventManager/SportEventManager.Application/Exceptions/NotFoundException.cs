namespace SportEventManager.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
         : base($"Sorry, we couldn't find the {entityName} with ID {key}. Please check and try again.") { }

    }
}
