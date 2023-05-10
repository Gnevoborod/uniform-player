using uniform_player.Controllers.Dtos.Interactive;
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


        public static Component FromInteractiveDtoToModelSimple(this ComponentInteractiveDto componentInteractiveDto)
        {
            if (componentInteractiveDto == null)
                return default!;
            return new Component
            {
                Name = componentInteractiveDto.Name,
                Type = (ComponentType)Enum.Parse(typeof(ComponentType), componentInteractiveDto.Type, true),
                Value = componentInteractiveDto.Value,
                Properties = componentInteractiveDto.Properties
            };
        }

        public static List<Component> FromDtoToModelList(this List<ComponentInteractiveDto> componentDto)
        {
            if (componentDto == null)
                return default!;
            List<Component> result = new List<Component>(componentDto.Count);
            foreach(var dto in componentDto)
            {
                result.Add(dto.FromInteractiveDtoToModelSimple());
            }
            return result;
        }

        public static ComponentInteractiveDto FromModelToDto(this Component component)
        {
            if (component == null)
                return default;
            return new ComponentInteractiveDto
            {
                Name = component.Name,
                Type = component.Type.ToString(),
                Value = component.Value,
                Properties = component.Properties
            };
        }

        public static List<ComponentInteractiveDto> FromModelToDtoList(this List<Component> component)
        {
            if (component == null)
                return default!;
            List<ComponentInteractiveDto> result = new List<ComponentInteractiveDto>();
            foreach(var cmp in component)
            {
                result.Add(cmp.FromModelToDto());
            }
            return result;
        }
    }
}
