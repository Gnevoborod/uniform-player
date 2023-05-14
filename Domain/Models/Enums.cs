namespace uniform_player.Domain.Models
{
    public enum ComponentType { Label, TextBox, RadioButton, CheckBox, TextArea, DateBox, Button }
    public enum Predicate { Equal, NotEqual, MoreThan, LessThan, Clicked }
    public enum ScreenType { Common };

    public enum ScenarioState { Draft, Published}
}
