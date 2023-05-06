namespace uniform_player.Infrastructure.Exceptions
{
    public class ExceptionEvents
    {
        public static EventId EnumValidatorNotEnum = new(1000, "Input type is not Enum");
        public static EventId EnumValidatorValidationFailed = new(1001, "The value is not defined in the enum. The value must be one of");
        public static EventId EnumValidatorValueIsNull = new(1002, "The value of enum expected, but null presented");

        public static EventId ScenarioNotPresented = new(1400, "The requested scenario doesn't presented in the list of scenarios");
    }
}
