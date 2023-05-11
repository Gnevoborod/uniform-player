using System.Linq;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Exceptions;

namespace uniform_player.Infrastructure.General
{
    public class ScenarioManager : IScenarioManager
    {
        private Dictionary<string, Scenario> scenarios;

        public ScenarioManager()
        {
            scenarios = new Dictionary<string, Scenario>();
        }
        public void AddScenario(string identity, Scenario scenario)
        {
            if (ContainsScenario(identity))
            {
                UpdateScenario(identity, scenario);
            }
            else
            {
                scenarios.Add(identity, scenario);
            }

        }

        public bool ContainsScenario(string identity)
        {
            return scenarios.ContainsKey(identity);
        }

        public Scenario GetScenario(string scenarioIdentity)
        {
            Scenario scenario;
            if (!scenarios.TryGetValue(scenarioIdentity, out scenario))
                throw new ApiException(ExceptionEvents.ScenarioNotPresented);
            return scenario;
        }

        public void UpdateScenario(string scenarioIdentity, Scenario scenario)
        {
            scenarios[scenarioIdentity] = scenario;
        }


        public Screen? GetSpecificScreen(string scenarioIdentity, string screenName)
        {
            return scenarios[scenarioIdentity].Screens.FirstOrDefault(s => s.Name == screenName);
        }

    }
}
