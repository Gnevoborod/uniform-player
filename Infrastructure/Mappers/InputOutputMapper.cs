using uniform_player.Controllers.Dtos.Interactive;
using uniform_player.Domain.Models;

namespace uniform_player.Infrastructure.Mappers
{
    public static class InputOutputMapper
    {
        public static InputOutput FromDtoToModel(this InputOutputDto inputOutputDto)
        {
            return new InputOutput
            {
                Screen = new ScreenInteractive
                {
                    Name = inputOutputDto.Screen.Name,
                    Type = (ScreenType)Enum.Parse(typeof(ScreenType), inputOutputDto.Screen.Type, true),
                    Title = inputOutputDto.Screen.Title,
                    Components = inputOutputDto.Screen.Components.ConvertAll(inputOutputDtoScreenComponent => new Component
                    {
                        Name = inputOutputDtoScreenComponent.Name,
                        Type = (ComponentType)Enum.Parse(typeof(ComponentType), inputOutputDtoScreenComponent.Type, true),
                        Value = inputOutputDtoScreenComponent.Value,
                        Properties = inputOutputDtoScreenComponent.Properties
                    })
                },
                PreviousScreens = inputOutputDto.PreviousScreens.ConvertAll(inputOutputDtoPreviousScreen => new PreviousScreen
                {
                    Screen = inputOutputDtoPreviousScreen.Screen,
                    Components = inputOutputDtoPreviousScreen.Components.ConvertAll(inputOutputDtoPreviousScreenComponent => new ComponentInteractive
                    {
                        Name = inputOutputDtoPreviousScreenComponent.Name,
                        Type = (ComponentType)Enum.Parse(typeof(ComponentType), inputOutputDtoPreviousScreenComponent.Type, true),
                        Value = inputOutputDtoPreviousScreenComponent.Value,
                        Properties = inputOutputDtoPreviousScreenComponent.Properties
                    })
                }),
                CurrentValues = new CurrentValues
                {
                    Screen = inputOutputDto.CurrentValues.Screen,
                    Components = inputOutputDto.CurrentValues.Components.ConvertAll(inputOutputDtoCurrentValuesComponent => new ComponentInteractive
                    {
                        Name = inputOutputDtoCurrentValuesComponent.Name,
                        Type = (ComponentType)Enum.Parse(typeof(ComponentType), inputOutputDtoCurrentValuesComponent.Type, true),
                        Value = inputOutputDtoCurrentValuesComponent.Value,
                        Properties = inputOutputDtoCurrentValuesComponent.Properties
                    })
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
