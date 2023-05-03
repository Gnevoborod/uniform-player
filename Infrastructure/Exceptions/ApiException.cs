namespace uniform_player.Infrastructure.Exceptions
{
    public class ApiException: Exception
    {
        public ApiException(EventId eventId):base(message:eventId.Name) 
        {
            HResult = eventId.Id;
        }
    }
}
