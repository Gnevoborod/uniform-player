namespace uniform_player.Infrastructure.Exceptions
{
    public class ExceptionEvents
    {
        public static EventId EnumValidatorNotEnum = new(1000, "Input type is not Enum");
        public static EventId EnumValidatorValidationFailed = new(1001, "The value is not defined in the enum. The value must be one of");
        public static EventId EnumValidatorValueIsNull = new(1002, "The value of enum expected, but null presented");

        public static EventId ScenarioNotPresented = new(1400, "The requested scenario doesn't presented in the list of scenarios");


        public static EventId RulesNotExists = new(1900, "There are no rules for these screen. Check scenario or screen name");
        public static EventId TransitionNotExists = new(1901, "Transition doesn't exist. Check the component's name or its value");
        public static EventId NoPreviousScreen= new(1902, "There is no previous screen to response (start of scenario was reached)");
        
    }
}
