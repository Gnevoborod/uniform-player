namespace uniform_player.Infrastructure.Exceptions
{
    public class ExceptionEvents
    {
        public static EventId EnumValidatorNotEnum = new(1000, "Input type is not Enum");
        public static EventId EnumValidatorValidationFailed = new(1001, "The value is not defined in the enum");

        public static EventId ScenarioNotPresented = new(1400, "The requested scenario doesn't presented in the list of scenarios");
    }
}
