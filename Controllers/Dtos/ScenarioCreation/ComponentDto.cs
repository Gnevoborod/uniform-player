using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class ComponentDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        [EnumSet(typeof(ComponentType))]
        public string Type { get; set; }
        public string Value { get; set; }
        public string? Properties { get; set; }
        public string? Label { get; set; }
    }
}
