namespace uniform_player.Domain.Models
{
    public class Screen
    {
        public string Name { get; set; } = default!;
        public ScreenType Type { get; set; }
        public string? Title { get; set; }
        public List<Component>? Components { get; set; }//надо или массив компонентов тут делать, или сам массив куда-то выносить чтоб его не потерять
        public string? Description { get; set; }

    }
}
