using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class RuleDto
    {
        public List<ConditionDto>? Conditions { get; set; }
        public string? NextScreen { get; set; }
    }
}
