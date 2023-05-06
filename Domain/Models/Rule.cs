
namespace uniform_player.Domain.Models
{
    public class Rule
    {
        public List<Condition>? Conditions { get; set; }
        public string? NextScreen { get; set; }
    }
}
