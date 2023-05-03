namespace uniform_player.Domain.Models
{
    public class Scenario
    {
        public string Identity { get; set; }
        public List<Screen> Screens { get; set; }
        //Внутри сценария оперируем только экранами. А внутри экранов уже списки компонентов и правила переходов.
    }
}
