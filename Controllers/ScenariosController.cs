using Microsoft.AspNetCore.Mvc;
using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.General;
using uniform_player.Infrastructure.Mappers;

namespace uniform_player.Controllers
{
    [Route("scenarios")]
    [Produces("application/json")]
    [ApiController]
    public class ScenariosController:Controller
    {
        private readonly IScenarioService _scenarioService;
        private readonly IScenarioManager _scenarioManager;
        private readonly ITransitionManager _transitionManager;
        public ScenariosController(IScenarioService scenarioService, IScenarioManager scenarioManager, ITransitionManager transitionManager) 
        {
            _scenarioService = scenarioService;
            _scenarioManager = scenarioManager;
            _transitionManager = transitionManager;
        }
        /// <summary>
        /// Отдаёт загруженный по данному идентификатору сценарий
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Scenario), 200)]
        [HttpGet("upload/{identity}")]
        public IActionResult GetScenario([FromRoute]string identity) 
        {
            //тут надо будет нормальный маппер потом сделать
            //var manager = ScenarioManager.GetInstance();
            //return Ok(_scenarioManager.GetScenario(identity));
            return Ok(_transitionManager.GetTransitions(identity));
        }


        /// <summary>
        /// Загружает сценарий для конкретного идентификатора
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="uploadScenarioDto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Scenario), 200)]
        [HttpPost("upload/{identity}")]
        public async Task<IActionResult> LoadNewScenario([FromRoute]string identity, [FromBody] UploadScenarioDto uploadScenarioDto)
        {
            Scenario scenario = new Scenario();
            scenario = uploadScenarioDto.FromDtoToModel();
            TransitionEngine transitionEngine = uploadScenarioDto.Transitions.MakeDictionary();
            _transitionManager.AddTransitions(identity, transitionEngine);
            _scenarioManager.AddScenario(identity, scenario);
            return Ok(transitionEngine);
        }


        /// <summary>
        /// Получает первый экран для конкретного сценария
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(InputOutputDto), 200)]
        [HttpGet("engine/getFirstScreen/{identity}")]
        public async Task<IActionResult> GetFirstScreen([FromRoute]string identity)
        {
            var firstScreen = _scenarioManager.GetFirstScreen(identity);
            InputOutput inputOutput = new InputOutput();

            return Ok(inputOutput.FromModelToDto(firstScreen));
        }

        /// <summary>
        /// Основной метод вычисления следующего экрана для перехода
        /// </summary>
        /// <param name="inputOutputDto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(InputOutputDto), 200)]
        [HttpPost("endine/getNextScreen")]
        public async Task<IActionResult> GetNextScreen([FromBody]InputOutputDto inputOutputDto)
        {
            return Ok();
        }

    }
}
