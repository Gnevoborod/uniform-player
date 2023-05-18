using System.Data;
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


        public Screen? GetNextScreen(CurrentValues currentValues, List<Transition> transitions, Scenario scenario)
        {
            if (transitions == null)
                throw new ApiException(ExceptionEvents.RulesNotExists);
            if (scenario == null)
                throw new ApiException(ExceptionEvents.ScenarioNotPresented);
            bool satisfied = false;
            foreach (var transition in transitions)
            {
                foreach (var value in currentValues.ComponentsValues)
                {
                    if (transition.Rules == null || transition.Rules.FirstOrDefault(r => r.ComponentName == value.ComponentName) == null)
                        continue;
                    foreach (var condition in transition.Rules)
                    {
                        satisfied = ConditionChecker.TestValue(condition.Predicate, value.Value!, condition.Value!);
                        if (satisfied)
                            break;
                    }
                    if (!satisfied)
                        break;
                }
                if (satisfied)
                    return scenario.Screens.FirstOrDefault(s => s.Name == transition.NextScreen);
            }
            throw new ApiException(ExceptionEvents.TransitionNotExists);
        }


    }
}
