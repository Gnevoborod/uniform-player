using System.Security.Principal;
using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Interfaces.Repositories;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Exceptions;
using uniform_player.Infrastructure.General;
using uniform_player.Infrastructure.Mappers;

namespace uniform_player.Infrastructure.Services
{
    public class ScenarioService:IScenarioService
    {
        private readonly ITransitionManager _transitionManager;
        private readonly IScenarioManager _scenarioManager;
        private readonly IScenarioRepository _scenarioRepository;
        public IMover _mover;
        private static bool dataLoaded;
        public ScenarioService(ITransitionManager transitionManager, IScenarioManager scenarioManager, IMover mover, IScenarioRepository scenarioRepository)
        {
            _transitionManager = transitionManager;
            _scenarioManager = scenarioManager;
            _scenarioRepository = scenarioRepository;
            _mover = mover;
            if (!dataLoaded)
            {
                LoadScenariosFromDb();
                dataLoaded = true;
            }
        }

        public async Task<TransitionEngine> PublishScenarioAsync(string identity, UploadScenarioDto uploadScenarioDto)//после завершения разработки транзишнэнжин - поменять на void
        {

            Scenario scenario = new Scenario();
            scenario = uploadScenarioDto.FromDtoToModel();
            TransitionEngine transitionEngine = uploadScenarioDto.Transitions.MakeDictionary();
            _transitionManager.AddTransitions(identity, transitionEngine);
            _scenarioManager.AddScenario(identity, scenario);
            await _scenarioRepository.SaveScenario(uploadScenarioDto.FromDtoToEntity(identity, ScenarioState.Published));
            return transitionEngine;
        }

        public Task SaveScenarioDraftAsync(string identity, UploadScenarioDto uploadScenarioDto)//после завершения разработки транзишнэнжин - поменять на void
        {
            return _scenarioRepository.SaveScenario(uploadScenarioDto.FromDtoToEntity(identity, ScenarioState.Draft));
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

        public InputOutputDto GetPreviousScreen(InputOutputDto inputOutputDto)
        {
            var lastPreviousScreen = inputOutputDto.PreviousScreens.LastOrDefault();
            if (lastPreviousScreen == null)
                throw new ApiException(ExceptionEvents.NoPreviousScreen);
            var screenToShow = _scenarioManager.GetSpecificScreen(inputOutputDto.Scenario, lastPreviousScreen.Screen);
            inputOutputDto.PreviousScreens.Remove(lastPreviousScreen);
            var components = lastPreviousScreen.ComponentsValues;
            inputOutputDto.Screen = screenToShow.FromModelToDto();
            foreach(var component in components)
            {
                var screenComponent = inputOutputDto.Screen.Components.FirstOrDefault(sc => sc.Name == component.ComponentName);
                if(screenComponent != null)
                    screenComponent.Value = component.Value;
            }
            inputOutputDto.CurrentValues = null;
            return inputOutputDto;
        }


        public void LoadScenariosFromDb()
        {
            var scenarios = _scenarioRepository.GetAllScenarios(ScenarioState.Published);
            if (scenarios == null)
                return;
            foreach(var scenario in scenarios)
            {
                var scenarioDto = scenario.FromEntityToDto();
                var scenarioModel = scenarioDto?.FromDtoToModel();
                if (scenarioDto == null || scenarioModel == null)
                    continue;
                TransitionEngine transitionEngine = scenarioDto.Transitions.MakeDictionary();
                _transitionManager.AddTransitions(scenario.Name, transitionEngine);
                _scenarioManager.AddScenario(scenario.Name, scenarioModel);
            }
        }
    }
}
