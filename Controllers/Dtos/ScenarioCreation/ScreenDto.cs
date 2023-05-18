using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class ScreenDto
    {
        public string Name { get; set; } = default!;
        [EnumSet(typeof(ScreenType))]
        public string Type { get; set; } = default!;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<ComponentDto> Components { get; set; } = default!;
        public List<TransitionDto>? Transitions { get; set; } = default!;
    }
}
