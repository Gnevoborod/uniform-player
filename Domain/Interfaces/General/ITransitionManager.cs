using uniform_player.Domain.Models;

namespace uniform_player.Domain.Interfaces.General
{
    public interface ITransitionManager
    {
        void AddTransitions(string scenarioIdentity, TransitionEngine transitionEngine);
        bool ContainsTransitions(string scenarioIdentity);
        TransitionEngine GetTransitions(string scenarioIdentity);
        void UpdateTransitions(string scenarioIdentity, TransitionEngine transitionEngine);
        public List<Rule> GetTransitionRulesForScreen(string scenarioIdentity, string screenName);
    }
}