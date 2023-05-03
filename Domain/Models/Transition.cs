
namespace uniform_player.Domain.Models
{
    public class Transition
    {
        public string ScreenSource { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
