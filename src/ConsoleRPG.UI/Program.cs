namespace ConsoleRPG.UI;

using System.CommandLine;
using ConsoleRPG;
using ConsoleRPG.Core;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Services;
using ConsoleRPG.UI.CommandOptions;
using ConsoleRPG.UI.Commands;
using ConsoleRPG.UI.Commands.Inventory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<ITextWriter, TextWriter>();
                services.AddSingleton<GameSession>();
                services.AddSingleton<MoveCommand>();
                services.AddSingleton<InGameCommand>();
                services.AddSingleton<CreateCharacterCommand>();
                services.AddSingleton<InventoryCommand>();
                services.AddSingleton<EquipCommand>();
                services.AddSingleton<DestroyCommand>();
                services.AddOptions<GameSettings>()
                    .Bind(config.GetSection("GameSettings"));
            })
            .Build();

        await host.StartAsync();
        var rootCommand = SetupCommandLine(host);

        rootCommand.Invoke("-h");
        return rootCommand.InvokeAsync(args).Result;
    }

    private static RootCommand SetupCommandLine(IHost host)
    {
        var gameStarted = false;
        var game = host.Services.GetService<GameSession>();
        var textWriter = host.Services.GetService<ITextWriter>();
        var inGameCommand = host.Services.GetService<InGameCommand>();
        var createCharacterCommand = host.Services.GetService<CreateCharacterCommand>();

        var rootCommand = new RootCommand("A command line driven RPG experience. Once the game has started commands " +
            "are executed in the context of the current command so you do not need to prefix with command i.e goto <location> ." +
            "And not ingame goto <location>.")
        {
            RootOptions.StartOption
        };

        rootCommand.AddCommand(inGameCommand!);
        rootCommand.AddCommand(createCharacterCommand!);
        rootCommand.AddGlobalOption(CommonOptions.QuitOption);
        rootCommand.SetHandler((bool startGame, bool endGame) =>
        {
            if (endGame)
            {
                Environment.Exit(1);
            }

            if (startGame)
            {
                Console.WriteLine(AsciArt.Title);
                gameStarted = true;

                CreateCharacter(game!, textWriter, createCharacterCommand);

                textWriter?.WriteLine($"Welcome to Sick Place {game.CurrentPlayer.Name}.");
                inGameCommand?.InvokeAsync("-h");
            }

            if (gameStarted)
            {
                while (gameStarted)
                {
                    PlayGame(inGameCommand!);
                }
            }
        }, RootOptions.StartOption, CommonOptions.QuitOption);

        return rootCommand;
    }

    private static void CreateCharacter(GameSession game, ITextWriter? textWriter, CreateCharacterCommand? createCharacterCommand)
    {
        while (string.IsNullOrWhiteSpace(game!.CurrentPlayer.Name))
        {
            textWriter!.WriteLine("Create your character.");
            createCharacterCommand!.InvokeAsync("-h");
            var command = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(command))
            {
                createCharacterCommand!.InvokeAsync("-h");
                continue;
            }
            createCharacterCommand?.Invoke(command);
        }
    }

    private static void PlayGame(InGameCommand inGameCommand)
    {
        string? command = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(command))
        {
            inGameCommand!.InvokeAsync("-h");
        }

        inGameCommand!.InvokeAsync(command!);
    }
}
