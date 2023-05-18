using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class RuleMapper
    {

        public static List<Rule> FromDtoToModelList(this List<RuleDto> dtos)
        {
            List<Rule> result = new List<Rule>();
            foreach(RuleDto dto in dtos)
            {
                var rule = dto.FromDtoToModel();
                result.Add(rule);
            }
            return result;
        }

        public static Rule FromDtoToModel(this RuleDto dto)
        {
            return new Rule
            {
                Description = dto.Description,
                ComponentName = dto.ComponentName,
                Predicate = (Predicate)Enum.Parse(typeof(Predicate), dto.Predicate, true),
                Value = dto.Value
            };
        }
    }
}
