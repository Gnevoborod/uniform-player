using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class ConditionMapper
    {
        public static Condition FromDtoToModel(this ConditionDto dto)
        {
            if (dto == null)
                return default!;
            Predicate predicate;
            Enum.TryParse(dto.Predicate, out predicate);
            return new Condition
            {
                Description = dto.Description,
                ComponentName = dto.ComponentName,
                Predicate = predicate,
                Value = dto.Value
            };
        }
    }
}
