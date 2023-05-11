using uniform_player.Domain.Models;

namespace uniform_player.Controllers.Dtos.Interactive
{
    public class InputOutputDto
    {
        public string Scenario { get; set; }
        public ScreenInteractiveDto Screen { get; set; }
        public List<PreviousScreenDto>? PreviousScreens { get; set; }
        public CurrentValuesDto? CurrentValues { get; set; }
    }
}
