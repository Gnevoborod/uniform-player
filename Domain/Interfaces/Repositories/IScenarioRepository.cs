using uniform_player.Database.Entities;
using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.Repositories
{
    public interface IScenarioRepository
    {
        public Task SaveScenario(ScenarioEntity scenarioEntity);
        public Task<ScenarioEntity?> GetScenario(string identity);
        public Task<List<ScenarioEntity>> GetAllScenariosAsync();
        public List<ScenarioEntity> GetAllScenarios(ScenarioState scenarioState);
    }
}
