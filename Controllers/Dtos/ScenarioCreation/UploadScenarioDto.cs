
namespace uniform_player.Controllers.Dtos.ScenarioCreation
{
    public class UploadScenarioDto
    {
        public ConfigurationDto Configuration { get; set; }
        public List<ScreenDto> Screens { get; set; }
        public List<ComponentDto> Components { get; set; }
        public List<TransitionDto> Transitions { get; set; }
    }
}
