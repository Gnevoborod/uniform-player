using System.Linq;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Exceptions;

namespace uniform_player.Infrastructure.General
{
    public class ScenarioManager
    {
        private Dictionary<string, Scenario> scenarios;
        private static readonly object _lock = new object();

        public ScenarioManager()
        {
            scenarios = new Dictionary<string, Scenario>();
        }
        public void AddScenario(string identity,Scenario scenario)
        {
            lock (_lock)
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
        }

        public bool ContainsScenario(string identity)
        {
            return scenarios.ContainsKey(identity);
        }

        public Scenario GetScenario(string identity)
        {
            Scenario scenario;
            if (!scenarios.TryGetValue(identity, out scenario))
                throw new ApiException(ExceptionEvents.ScenarioNotPresented);
            return scenario;
        }

        public void UpdateScenario(string identity,Scenario scenario)
        {
            scenarios[identity]=scenario;
        }


        public Screen? GetFirstScreen(string identity)
        {
            if (!ContainsScenario(identity))
                return default!;
            return scenarios[identity]
                    .Screens
                    .FirstOrDefault(s=>s.Name == scenarios[identity]
                    .FirstScreen);
        }

        public Screen? GetNextScreen(string identity, CurrentValues currentValues)
        {
            if (!ContainsScenario(identity))
                return default!;
            return new();
        }

    }
}
