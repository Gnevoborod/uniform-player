using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class ScreenDto
    {
        public string Name { get; set; }
        [EnumSet(typeof(ScreenType))]
        public string Type { get; set; }
        public string? Title { get; set; }
        public List<string>? Body { get; set; }
        public List<string>? PseudoName { get; set; }
        public string? Description { get; set; }
    }
}
