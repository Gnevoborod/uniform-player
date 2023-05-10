using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class CurrenValuesMapper
    {
        public static PreviousScreen FromCurrenValuesModelToPreiousScreenModel(this CurrentValues currentValues)
        {
            if (currentValues == null) return default!;
            return new PreviousScreen
            {
                Screen = currentValues.Screen,
                ComponentsValues = currentValues.ComponentsValues.ToList()
            };
        }

        public static PreviousScreenDto FromCurrenValuesDtoToPreiousScreenDto(this CurrentValuesDto currentValues)
        {
            if (currentValues == null) return default!;
            return new PreviousScreenDto
            {
                Screen = currentValues.Screen,
                ComponentsValues = currentValues.ComponentsValues.ToList()
            };
        }
    }
}
