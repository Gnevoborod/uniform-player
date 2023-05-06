using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Validators;

namespace uniform_player.Controllers.Dtos.Interactive
{
    public class ScreenInteractiveDto
    {
        public string Name { get; set; }
        [EnumSet(typeof(ScreenType))]
        public string Type { get; set; }
        public string Title { get; set; }
        public List<ComponentInteractiveDto> Components { get; set; }
    }
}
