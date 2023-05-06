using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class RuleMapper
    {

        public static List<Rule> FromDtoToModelList(this List<RuleDto> dtos)
        {
            List<Rule> result = new List<Rule>();
            foreach (RuleDto dto in dtos)
            {
                var rule = dto.FromDtoToModel();
                if (dto.Conditions != null)
                {
                    if (dto.Conditions.Count > 0)
                        rule.Conditions = new List<Condition>();
                    foreach (var condition in dto.Conditions)
                    {
                        rule.Conditions?.Add(condition.FromDtoToModel());
                    }
                }
                result.Add(rule);
            }
            return result;
        }

        public static Rule FromDtoToModel(this RuleDto dto)
        {
            if (dto == null)
                return default!;
            return new Rule
            {
                Conditions = null,//тут должна быть норм логика
                NextScreen = dto.NextScreen
            };
        }
    }
}
