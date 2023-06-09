﻿namespace uniform_player.Domain.Models
{
    public class Rule
    {
        public string? Description { get; set; }
        public string ComponentName { get; set; } = default!;
        public Predicate Predicate { get; set; }
        public string? Value { get; set; }
    }
}
