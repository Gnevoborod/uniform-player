using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class ScreenMapper
    {
        public static Screen FromDtoToModel(this ScreenDto screenDto)
        {
            if (screenDto == null)
                return default!;
            return new Screen
            {
                Name = screenDto.Name,
                Type = screenDto.Type,
                Title = screenDto.Title,
                Body = screenDto.Body,
                PseudoName = screenDto.PseudoName,
                Description = screenDto.Description
            };
        }

        public static ScreenDto FromModelToDto(this Screen screen)
        {
            if (screen == null)
                return default!;
            return new ScreenDto
            {
                Name = screen.Name,
                Type = screen.Type,
                Title = screen.Title,
                Body = screen.Body,
                PseudoName = screen.PseudoName,
                Description = screen.Description
            };
        }
    }
}
