using System.CommandLine;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRPG.UI.Commands.Combat;

internal class AttackCommand : Command
{
    public AttackCommand(ITextWriter textWriter, IServiceProvider serviceProvider) 
        : base(CommandNames.Attack, "Attack with your current weapon.")
    {
        this.SetHandler(() => {
            var gameSession = serviceProvider.GetService<GameSession>();

            if (!gameSession!.InCombat)
            {
                textWriter.WriteLine("You are not in combat!");
            }

            //TODO: The logic for this should live in the core and not in the UI layer
            var currentPlayer = gameSession.CurrentPlayer;
            var currentMonster = gameSession!.CurrentLocation!.Monster;
            var playerAttack = currentPlayer.Attack - currentMonster!.Defense;
            var monsterAttack = currentMonster.Attack - currentPlayer.Defense;

            currentMonster!.Health -= playerAttack;
            textWriter.WriteLine($"You did {currentPlayer.Attack} damage to {currentMonster.Name}", 
                ConsoleColor.Green);
            currentPlayer.Health -= monsterAttack;
            textWriter.WriteLine($"You took {currentMonster.Attack} damage from {currentMonster.Name}", 
                ConsoleColor.Red);

            if (currentPlayer.Health <= 0)
            {
                textWriter.WriteLine($"You have died!");
                
                // TODO: To decide what to do can reset game or revive in town.
                Environment.Exit(1);
            }

            if (currentMonster.Health <= 0)
            {
                textWriter.WriteLine($"{currentMonster.Name} has been killed.", ConsoleColor.Green);
                gameSession.CurrentLocation.Monster = null;
                gameSession.InCombat = false;
            }
        });
    }
}
