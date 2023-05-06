using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.General
{
    public interface IScenarioManager
    {
        void AddScenario(string identity, Scenario scenario);
        bool ContainsScenario(string identity);
        Screen? GetFirstScreen(string identity);
        Screen? GetNextScreen(string identity, CurrentValues currentValues);
        Scenario GetScenario(string identity);
        void UpdateScenario(string identity, Scenario scenario);
    }
}