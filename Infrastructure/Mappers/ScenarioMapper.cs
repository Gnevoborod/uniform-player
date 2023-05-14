using System.Text.Json;
using System.Text.Json.Serialization;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Database.Entities;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Mappers;
namespace uniform_player.Infrastructure.Mappers
{
    public static class ScenarioMapper
    {
        public static Scenario FromDtoToModel(this UploadScenarioDto dto)
        {
            if (dto == null)
                return default!;
            
            var scenario = new Scenario();
            scenario.FirstScreen = dto.Configuration.FirstScreen;
            List<Screen> resultScreen = new List<Screen>();
            scenario.Screens = resultScreen;
            foreach (ScreenDto screenDto in dto.Screens)
            {
                Screen screen = screenDto.FromDtoToModel();
                if (screenDto.Components != null)
                {
                    foreach (var components in screenDto.Components)
                    {
                        if (screen.Components == null)
                            screen.Components = new List<Component>(screenDto.Components.Count);
                        var nextToAdd = dto.Components?.FirstOrDefault(cd => cd.Name == components)?.FromDtoToModel();
                        if (nextToAdd != null)
                            screen.Components.Add(nextToAdd);
                    }
                }
                resultScreen.Add(screen);
                if (screen.PseudoName != null)
                {
                    int count = screen.PseudoName.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var screenNext = screen.MakePseudoNamedScreen(screen.PseudoName[i]);
                        resultScreen.Add(screenNext);
                    }
                }

            }
            return scenario;

        }

        public static ScenarioEntity FromDtoToEntity(this UploadScenarioDto dto, string scenarioIdentity, ScenarioState scenarioState)
        {
            if (dto == null)
                return default!;
            if (string.IsNullOrEmpty(scenarioIdentity))
                return default!;
            return new ScenarioEntity
            {
                ScenarioState = scenarioState,
                Name = scenarioIdentity,
                Body = JsonSerializer.Serialize(dto)
            };
        }

        public static UploadScenarioDto? FromEntityToDto(this ScenarioEntity scenarioEntity)
        {
            if(scenarioEntity == null || scenarioEntity.Body == null)
                    return default!;
            return JsonSerializer.Deserialize<UploadScenarioDto>(scenarioEntity.Body);
        }
    }
}
