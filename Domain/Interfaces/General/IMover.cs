using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.General
{
    public interface IMover
    {
        public Screen? GetNextScreen(CurrentValues currentValues, List<Transition> transitions, Scenario scenario);
        public Screen? GetFirstScreen(Scenario scenario);
    }
}