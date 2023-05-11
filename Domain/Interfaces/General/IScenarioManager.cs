using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.General
{
    public interface IScenarioManager
    {
        void AddScenario(string identity, Scenario scenario);
        bool ContainsScenario(string identity);
        Scenario GetScenario(string identity);
        void UpdateScenario(string identity, Scenario scenario);
        public Screen? GetSpecificScreen(string identity, string screenName);
    }
}