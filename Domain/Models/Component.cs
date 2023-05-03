namespace uniform_player.Domain.Models
{
    public class Component
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string? Properties { get; set; }
        public List<string>? PseudoName { get; set; }
    }
}
