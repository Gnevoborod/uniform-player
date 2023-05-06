using Microsoft.Extensions.Primitives;

namespace uniform_player.Domain.Models
{
    /// <summary>
    /// Специальный класс хранящий в себе набор переходов
    /// </summary>
    public class TransitionEngine
    {
        public Dictionary<string, List<Rule>> Transitions { get; set; } = default!;
    }
}
