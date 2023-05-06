namespace uniform_player.Infrastructure.Exceptions
{
    public class ApiException: Exception
    {
        public ApiException(EventId eventId):base(message:eventId.Name) 
        {
            HResult = eventId.Id;
        }

        public ApiException(EventId eventId, string exceptionMessage):base(message: eventId.Name +" " + exceptionMessage) 
        {
            HResult = eventId.Id;
        }
    }
}
