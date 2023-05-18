using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class TransitionEngineMapper
    {
        public static Transition FromDtoToModel(this TransitionDto transitionDto)
        {
            if(transitionDto == null)
                    return default!;
            Transition transition = new Transition();
            transition.NextScreen = transitionDto.NextScreen;
            transition.Rules = new List<Rule>();
            foreach(var rule in transitionDto.Rules)
            {
                var newRule = rule.FromDtoToModel();
                transition.Rules.Add(newRule);
            }
            return transition;
        }

        public static List<Transition> FromDtoToModelList(this List<TransitionDto> transitionsDto)
        {
            if (transitionsDto == null)
                return default!;
            List<Transition> result = new List<Transition>();
            foreach (var transition in transitionsDto)
            {
                var newTransition = transition.FromDtoToModel();
                result.Add(newTransition);
            }
            return result;
        }

        public static int MakeDictionary(this TransitionEngine transitionEngine, UploadScenarioDto uploadDto)
        {
            if (transitionEngine == null)
                return default!;
            if(transitionEngine.Transitions == null)
                transitionEngine.Transitions = new();
            if (uploadDto == null)
                return default!;
            foreach(var screen in uploadDto.Screens)
            {
                var newTransitions = screen.Transitions.FromDtoToModelList();
                transitionEngine.Transitions.Add(screen.Name, newTransitions);
            }
            return transitionEngine.Transitions.Count();
        }

    }
}
