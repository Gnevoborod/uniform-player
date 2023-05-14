using Microsoft.EntityFrameworkCore;
using uniform_player.Database;
using uniform_player.Database.Entities;
using uniform_player.Domain.Interfaces.Repositories;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Repositories
{
    public class ScenarioRepository:IScenarioRepository
    {
        private readonly UniformPlayerDbContext _context;

        public ScenarioRepository(UniformPlayerDbContext context)
        {
            _context = context;
        }

        public async Task SaveScenario(ScenarioEntity scenarioEntity)
        {
            var entity = await _context.ScenarioEntity.FirstOrDefaultAsync(se=>se.Name == scenarioEntity.Name);
            if (entity == null)
            {
                _context.ScenarioEntity.Add(scenarioEntity);
            }
            else
            {
                entity.Body = scenarioEntity.Body;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<ScenarioEntity?> GetScenario(string identity)
        {
            var scenario = await _context.ScenarioEntity.FirstOrDefaultAsync(se=>se.Name ==  identity);
            return scenario;
        }

        public Task<List<ScenarioEntity>> GetAllScenariosAsync()
        {
            return _context.ScenarioEntity.ToListAsync();
        }

        public Task<List<ScenarioEntity>> GetAllScenariosAsync(ScenarioState scenarioState)
        {
            return _context.ScenarioEntity.Where(se=>se.ScenarioState == scenarioState).ToListAsync();
        }
    }
}
