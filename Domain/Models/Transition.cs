
namespace uniform_player.Domain.Models
{
    public class Transition
    {
        public List<Rule>? Rules { get; set; }
        public string? NextScreen { get; set; }
    }
}
