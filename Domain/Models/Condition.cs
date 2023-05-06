namespace uniform_player.Domain.Models
{
    public enum Predicate { Equal, NotEqual, MoreThan, LessThan, Clicked }
    public enum ConditionType { AND, OR }
    public class Condition
    {
        public string? Description { get; set; }
        public string ComponentName { get; set; }
        public Predicate Predicate { get; set; }
        public string? Value { get; set; }
    }
}
