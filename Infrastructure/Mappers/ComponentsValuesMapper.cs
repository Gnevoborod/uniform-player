using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static  class ComponentsValuesMapper
    {
        public static ComponentsValues FromDtoToModel(this ComponentsValuesDto componentsValuesDto)
        {
            if (componentsValuesDto == null)
                return default!;
            return new ComponentsValues
            {
                ComponentName = componentsValuesDto.ComponentName,
                Value = componentsValuesDto.Value
            };
        }

        public static List<ComponentsValues> FromDtoToModelList(this List<ComponentsValuesDto> componentsValuesDto)
        {
            if(componentsValuesDto == null)
                return default!;
            List<ComponentsValues> result = new List<ComponentsValues>();
            foreach(var componentDto in componentsValuesDto)
            {
                result.Add(componentDto.FromDtoToModel());
            }
            return result;
        }

        public static ComponentsValuesDto FromModelToDto(this ComponentsValues componentsValues)
        {
            return new ComponentsValuesDto
            {
                ComponentName = componentsValues.ComponentName,
                Value = componentsValues.Value
            };
        }

        public static List<ComponentsValuesDto> FromModelToDtoList(this List<ComponentsValues> componentsValues)
        {
            if (componentsValues == null)
                return default!;
            List<ComponentsValuesDto> result = new List<ComponentsValuesDto>();
            foreach (var component in componentsValues)
            {
                result.Add(component.FromModelToDto());
            }
            return result;
        }

    }
}
