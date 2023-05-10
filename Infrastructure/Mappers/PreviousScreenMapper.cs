using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class PreviousScreenMapper
    {

        public static PreviousScreenDto FromModelToDto(this PreviousScreen previousScreen)
        {
            return new PreviousScreenDto
            {
                Screen = previousScreen.Screen,
                ComponentsValues = previousScreen.ComponentsValues.FromModelToDtoList()
            };
        }

        public static PreviousScreen FromDtoToModel(this PreviousScreenDto previousScreen)
        {
            if (previousScreen == null)
                return default;
            return new PreviousScreen
            {
                Screen = previousScreen.Screen,
                ComponentsValues = previousScreen.ComponentsValues.FromDtoToModelList()
            };
        }


        public static List<PreviousScreenDto> FromModelToDtoList(this List<PreviousScreen> previousScreens) 
        {
            if (previousScreens == null)
                return default!;
            List<PreviousScreenDto> result = new List<PreviousScreenDto>();
            foreach(var previousScreen in previousScreens)
            {
                result.Add(previousScreen.FromModelToDto());
            }
            return result;
        }

        public static List<PreviousScreen> FromDtoToModelList(this List<PreviousScreenDto> previousScreensDto)
        {
            if (previousScreensDto == null)
                return default!;
            List<PreviousScreen> result = new List<PreviousScreen>();
            foreach (var previousScreen in previousScreensDto)
            {
                result.Add(previousScreen.FromDtoToModel());
            }
            return result;
        }

        
    }
}
