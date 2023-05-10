using System.Security.Principal;
using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.General;
using uniform_player.Infrastructure.Mappers;

namespace uniform_player.Infrastructure.Services
{
    public class ScenarioService:IScenarioService
    {
        private readonly ITransitionManager _transitionManager;
        private readonly IScenarioManager _scenarioManager;
        public IMover _mover;
        public ScenarioService(ITransitionManager transitionManager, IScenarioManager scenarioManager, IMover mover)
        {
            _transitionManager = transitionManager;
            _scenarioManager = scenarioManager;
            _mover = mover;
        }

        public TransitionEngine LoadNewScenario(string identity, UploadScenarioDto uploadScenarioDto)//после завершения разработки транзишнэнжин - поменять на void
        {
            Scenario scenario = new Scenario();
            scenario = uploadScenarioDto.FromDtoToModel();
            TransitionEngine transitionEngine = uploadScenarioDto.Transitions.MakeDictionary();
            _transitionManager.AddTransitions(identity, transitionEngine);
            _scenarioManager.AddScenario(identity, scenario);
            return transitionEngine;
        }

        public InputOutputDto GetNextScreen(InputOutputDto inputOutputDto)
        {
            var rules = _transitionManager.GetTransitionRulesForScreen(inputOutputDto.Scenario, inputOutputDto.CurrentValues.Screen);
            var scenario = _scenarioManager.GetScenario(inputOutputDto.Scenario);
            inputOutputDto.Screen = _mover.GetNextScreen(inputOutputDto.FromDtoToModel().CurrentValues, rules, scenario).FromModelToDto();
            if(inputOutputDto.PreviousScreens == null)
            {
                inputOutputDto.PreviousScreens = new List<PreviousScreenDto>();
            }
            inputOutputDto.PreviousScreens.Add(inputOutputDto.CurrentValues.FromCurrenValuesDtoToPreiousScreenDto());
            inputOutputDto.CurrentValues = null;
            return inputOutputDto;
        }

        public InputOutputDto GetFirstScreen(string scenarioIdentity)
        {
            var scenario = _scenarioManager.GetScenario(scenarioIdentity);
            var screen = _mover.GetFirstScreen(scenario);
            return new InputOutputDto()
            {
                Scenario=scenarioIdentity,
                Screen = screen.FromModelToDto(),
                PreviousScreens = null,
                CurrentValues = null
            };
        }
    }
}
