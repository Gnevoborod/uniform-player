using uniform_player.Controllers.Dtos.ScenarioCreation;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class TransitionEngineMapper
    {
        public static Transition FromDtoToModel(this TransitionDto transitionDto)
        {
            if (transitionDto == null)
                return default!;
            Transition transition = new Transition();
            transition.ScreenSource = transitionDto.ScreenSource;
            transition.Rules = new List<Rule>(transitionDto.Rules.Count);
            foreach(var rule in transitionDto.Rules) 
            {
                var newRule = rule.FromDtoToModel();
                transition.Rules.Add(newRule);
            }
            return transition;
        }

        public static TransitionEngine MakeDictionary(this List<TransitionDto> Transitions)
        {
            TransitionEngine transitionEngine = new TransitionEngine();
            transitionEngine.Transitions = new Dictionary<string, List<Rule>>();
            foreach (var transition in Transitions)
            {
                string screenName = transition.ScreenSource;
                List<Rule> rules = transition.Rules.FromDtoToModelList();
                transitionEngine.Transitions.Add(screenName,rules);
            }
            return transitionEngine;
        }
    }
}
