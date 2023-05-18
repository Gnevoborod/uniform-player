using System.Linq;
using System.Xml;
using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class ScreenMapper
    {
        public static Screen FromDtoToModel(this ScreenDto dto)
        {
            if (dto == null)
                return default!;
            ScreenType screenType;
            Enum.TryParse(dto.Type, out screenType);
            return new Screen
            {
                Name = dto.Name,
                Type = screenType,
                Title = dto.Title,
                Components = dto.Components.FromDtoToModelListScenario(),//сюда изначально ничего не пишем, так как в методе маппинга добавляем сюда лист компонентов
                //PseudoName = dto.PseudoName,
                Description = dto.Description

            };
        }

        public static Screen MakePseudoNamedScreen(this Screen screen, string pseudoName)
        {
            if (screen == null)
                return default!;
            return new Screen()
            {
                Name = pseudoName,
                Type = screen.Type,
                Title = screen.Title,
                Components = screen.Components,
                //PseudoName = screen.PseudoName,
                Description = screen.Description
            };
        }


        public static ScreenInteractiveDto FromModelToDto(this Screen screen)
        {
            if (screen == null)
                return default!;
            return new ScreenInteractiveDto
            {
                Name = screen.Name,
                Type = screen.Type.ToString(),
                Title = screen.Title,
                Components = screen.Components.FromModelToDtoList()
            };
        }
    }
}
