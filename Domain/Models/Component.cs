﻿namespace uniform_player.Domain.Models
{
    public enum ComponentType {Label, TextBox, RadioButton, CheckBox, TextArea, DateTime, Button }
    public class Component
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ComponentType Type { get; set; }
        public string Value { get; set; }
        public string? Properties { get; set; }
    }
}
