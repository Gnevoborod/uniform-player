using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class InputOutputMapper
    {
        //Маппить только вручную.
        public static InputOutput FromDtoToModel(this InputOutputDto inputOutputDto)
        {
            return new InputOutput
            {
                Scenario = inputOutputDto.Scenario,
                Screen = new ScreenInteractive
                {
                    Name = inputOutputDto.Screen.Name,
                    Type = (ScreenType)Enum.Parse(typeof(ScreenType), inputOutputDto.Screen.Type, true),
                    Title = inputOutputDto.Screen.Title,
                    Components = inputOutputDto.Screen.Components.FromDtoToModelList()
                },
                PreviousScreens = inputOutputDto.PreviousScreens?.FromDtoToModelList(),
                CurrentValues = new CurrentValues
                {
                    Screen = inputOutputDto.CurrentValues.Screen,
                    ComponentsValues = inputOutputDto.CurrentValues.ComponentsValues.FromDtoToModelList()
                }
            };
        }

        public static InputOutputDto FromModelToDto(this InputOutput io, Screen screen)
        {
            return new InputOutputDto
            {
                Screen = new ScreenInteractiveDto
                {
                    Name = screen.Name,
                    Type = screen.Type.ToString(),
                    Title = screen.Title ?? throw new NullReferenceException("The value of 'screen.Title' should not be null"),
                    Components = screen.Components?.ConvertAll(screenComponent => new ComponentInteractiveDto
                    {
                        Name = screenComponent.Name,
                        Type = screenComponent.Type.ToString(),
                        Value = screenComponent.Value,
                        Properties = screenComponent.Properties ?? throw new NullReferenceException("The value of 'screenComponent.Properties' should not be null")
                    }) ?? throw new NullReferenceException("The value of 'screen.Components' should not be null")
                }
            };
        }
    }
}
