﻿
namespace uniform_player.Domain.Models
{
    public class InputOutput
    {
        public string Scenario { get; set; }
        public ScreenInteractive Screen { get; set; }
        public List<PreviousScreen>? PreviousScreens { get; set; }
        public CurrentValues CurrentValues { get; set; }
    }
}
