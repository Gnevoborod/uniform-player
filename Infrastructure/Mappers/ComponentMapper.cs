using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class ComponentMapper
    {
        public static Component FromDtoToModel(this ComponentDto componentDto)
        {
            if (componentDto == null)
                return default!;
            return new Component
            {
                Name = componentDto.Name,
                Description = componentDto.Description,
                Type = (ComponentType)Enum.Parse(typeof(ComponentType), componentDto.Type, true),
                Value = componentDto.Value,
                Properties = componentDto.Properties
            };
        }
    }
}
