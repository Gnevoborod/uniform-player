using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class ConditionDto
    {
        public string? Description { get; set; }
        public string ComponentName { get; set; }
        [EnumSet(typeof(Predicate))]
        public string Predicate { get; set; }
        public string? Value { get; set; }
    }
}
