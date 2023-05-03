using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class RuleDto
    {
        [EnumSet(typeof(ConditionType))]
        public string ConditionType { get; set; }
        public List<ConditionDto>? Conditions { get; set; }
        public string? NextScreen { get; set; }
    }
}
