using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Models;
using uniform_player.Infrastructure.Exceptions;

namespace uniform_player.Infrastructure.General
{
    //Класс непосредственно осуществляющий перемещение между экранами
    public class Mover : IMover
    {

        public Screen? GetFirstScreen(Scenario scenario)
        {
            if (scenario == null)
                return new();
            return scenario
                    .Screens
                    .FirstOrDefault(s => s.Name == scenario
                    .FirstScreen);
        }

        public Screen? GetNextScreen(CurrentValues currentValues, List<Rule> rules, Scenario scenario)
        {
            if (rules==null)
                throw new ApiException(ExceptionEvents.RulesNotExists);
            if(scenario == null)
                throw new ApiException(ExceptionEvents.ScenarioNotPresented);
            bool satisfied = false;
            foreach(var rule in rules)
            {
                if(rule.Conditions == null) continue;
                foreach (var value in currentValues.ComponentsValues)
                {
                    if (rule.Conditions.FirstOrDefault(r=>r.ComponentName == value.ComponentName) == null)
                        break;
                    foreach (var condition in rule.Conditions)
                    {
                        satisfied = ConditionChecker.TestValue(condition.Predicate, value.Value, condition.Value!);
                    }
                    if (!satisfied)
                        break;
                }
                if (satisfied)
                    return scenario.Screens.FirstOrDefault(s => s.Name == rule.NextScreen);
            }
            throw new ApiException(ExceptionEvents.TransitionNotExists);
        }
    }
}
