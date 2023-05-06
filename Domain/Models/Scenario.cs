namespace uniform_player.Domain.Models
{
    public class Scenario
    {
        public string FirstScreen { get; set; } = default!;
        public List<Screen> Screens { get; set; } = default!;
        //Внутри сценария оперируем только экранами. А внутри экранов уже списки компонентов и правила переходов.
    }
}
