
namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class UploadScenarioDto
    {
        public ConfigurationDto Configuration { get; set; } = default!;
        public List<ScreenDto> Screens { get; set; } = default!;
        public List<ComponentDto> Components { get; set; } = default!;
        public List<TransitionDto> Transitions { get; set; } = default!;
    }
}
