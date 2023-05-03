
namespace uniform_player.Domain.Models
{
    public class Rule
    {
        public string ConditionType { get; set; }
        public List<Condition> Conditions { get; set; }
        public string NextScreen { get; set; }
    }
}
