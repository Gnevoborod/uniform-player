using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.Interactive
{
    public class ComponentInteractiveDto
    {
        public string Name { get; set; }
        [EnumSet(typeof(ComponentType))]
        public string Type { get; set; }
        public string Value { get; set; }
        public string? Properties { get; set; }
    }
}
