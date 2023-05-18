using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class TransitionDto
    {
        public List<RuleDto>? Rules { get; set; }
        public string? NextScreen { get; set; }
    }
}
