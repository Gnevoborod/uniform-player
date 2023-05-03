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
                Type = componentDto.Type,
                Value = componentDto.Value,
                Properties = componentDto.Properties,
                PseudoName = componentDto.PseudoName
            };
        }

        public static ComponentDto FromModelToDto(this Component component)
        {
            if (component == null) 
                return default!;
            return new ComponentDto
            {
                Name = component.Name,
                Description = component.Description,
                Type = component.Type,
                Value = component.Value,
                Properties = component.Properties,
                PseudoName = component.PseudoName
            };
        }
    }
}
