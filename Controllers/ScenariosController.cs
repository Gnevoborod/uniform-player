using Microsoft.AspNetCore.Mvc;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Interfaces.Services;

namespace uniform_player.Controllers
{
    [Route("scenarios")]
    [Produces("application/json")]
    [ApiController]
    public class ScenariosController:Controller
    {
        private readonly IScenarioService _scenarioService;
        public ScenariosController(IScenarioService scenarioService) 
        {
            _scenarioService = scenarioService;
        }

        [HttpPost("upload/{identity}")]
        public async Task<IActionResult> LoadNewScenario([FromRoute]string identity, [FromBody] UploadScenarioDto uploadScenarioDto)
        {
            return Ok(identity);
        }

    }
}
