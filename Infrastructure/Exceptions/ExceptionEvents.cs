namespace uniform_player.Infrastructure.Exceptions
{
    public class ExceptionEvents
    {
        public static EventId EnumValidatorNotEnum = new(100, "Input type is not Enum");
        public static EventId EnumValidatorValidationFailed = new(101, "The value is not defined in the enum");
    }
}
