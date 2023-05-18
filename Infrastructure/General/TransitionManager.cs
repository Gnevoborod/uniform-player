using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.General
{
    public class TransitionManager : ITransitionManager
    {
        private Dictionary<string, TransitionEngine> scenarioTransitions;//содержит словарь по каждому сценарию, внутри которого словарь для каждого экрана с переходами
        public readonly object obj=new object();
        public TransitionManager()
        {
            scenarioTransitions = new Dictionary<string, TransitionEngine>();
        }

        /// <summary>
        /// Добавляем правило перехода для конкретного сценария. Если сценария не существует - добавляем. Если перехода не существует - добавляем переход и правило
        /// </summary>
        /// <param name="scenarioIdentity"></param>
        /// <param name="transitionEngine"></param>
        public void AddTransitions(string scenarioIdentity, TransitionEngine transitionEngine)
        {
            lock (obj)
            {
                if (ContainsTransitions(scenarioIdentity))
                {
                    UpdateTransitions(scenarioIdentity, transitionEngine);
                }
                else
                {
                    scenarioTransitions.Add(scenarioIdentity, transitionEngine);
                }
            }
        }

        public bool ContainsTransitions(string scenarioIdentity)
        {
            return scenarioTransitions.ContainsKey(scenarioIdentity);//просто смотрим что для данного сценария зарегистрированы (или нет) переходы
        }

        public void UpdateTransitions(string scenarioIdentity, TransitionEngine transitionEngine)
        {
            scenarioTransitions[scenarioIdentity] = transitionEngine;
        }

        public TransitionEngine GetTransitions(string scenarioIdentity)
        {
            return scenarioTransitions[scenarioIdentity];
        }

        public List<Transition> GetTransitionRulesForScreen(string scenarioIdentity, string screenName)
        {
            var transitionEngine = GetTransitions(scenarioIdentity);
            return transitionEngine.Transitions[screenName];
        }
    }
}
