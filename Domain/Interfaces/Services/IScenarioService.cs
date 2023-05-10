﻿using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.Services
{
    public interface IScenarioService
    {
        public TransitionEngine LoadNewScenario(string identity, UploadScenarioDto uploadScenarioDto);

        public InputOutputDto GetNextScreen(InputOutputDto inputOutputDto);
        public InputOutputDto GetFirstScreen(string scenarioIdentity);
    }
}
