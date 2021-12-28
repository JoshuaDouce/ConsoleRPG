using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.UI.CommandOptions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands;

internal class CreateCharacterCommand : Command
{
    public CreateCharacterCommand(ITextWriter textWriter, IServiceProvider serviceProvider) : 
        base(CommandNames.CreateChracaterCommand, "Creates your character")
    {
        AddOption(CharacterOptions.CharacterName);

        this.SetHandler((string name, bool quit) => {
            if (quit)
            {
                Environment.Exit(1);
            }

            var gameSession = serviceProvider.GetService<GameSession>();

            if (string.IsNullOrWhiteSpace(name))
            {
                textWriter.WriteLine("Please provide a valid name");
                return;
            }

            gameSession!.CurrentPlayer.Name = name;
        
        }, CharacterOptions.CharacterName, CommonOptions.QuitOption);
    }
}
