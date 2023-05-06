namespace uniform_player.Domain.Models
{
    public class ScreenInteractive
    {
        public string Name { get; set; }
        public ScreenType Type { get; set; }
        public string? Title { get; set; }
        public List<Component>? Components { get; set; }

    }
}
