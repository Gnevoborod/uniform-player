using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.General
{
    public class TransitionManager
    {
        private Dictionary<string, List<Rule>> transitions;
        private static readonly object _lock = new object();

        public TransitionManager()
        {
            transitions = new Dictionary<string, List<Rule>>();
        }

        /// <summary>
        /// Добавляем правило перехода. Если перехода (как transition, а не правила) не существует - добавляем переход и правило
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="rule"></param>
        public void AddRule(string identity, Rule rule)
        {
            lock (_lock)
            {
                if(!ContainsTransition(identity))
                {
                    transitions.Add(identity, new List<Rule>());
                }
                transitions[identity].Add(rule);
            }
        }
        
        public bool ContainsTransition(string identity) 
        {
            return transitions.ContainsKey(identity);
        }
    }
}
