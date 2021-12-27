namespace ConsoleRPG.UI;

using System.CommandLine;
using ConsoleRPG;
using ConsoleRPG.Core;
using ConsoleRPG.UI.CommandOptions;
using ConsoleRPG.UI.Commands;
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
                services.AddScoped<ITextWriter, TextWriter>();
                services.AddSingleton<GameSession>();
                services.AddSingleton<MoveCommand>();
                services.AddSingleton<InGameCommand>();
                services.AddSingleton<CreateCharacterCommand>();
                services.AddOptions<GameSettings>()
                    .Bind(config.GetSection("GameSettings"));
            })
            .Build();

        await host.StartAsync();
        var rootCommand = SetupCommandLine(host);

        rootCommand.Invoke("-h");
        var command = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(command))
        {
            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        return rootCommand.InvokeAsync("-h").Result;
    }

    private static RootCommand SetupCommandLine(IHost host)
    {
        var gameStarted = false;
        var game = host.Services.GetService<GameSession>();
        var textWriter = host.Services.GetService<ITextWriter>();

        var rootCommand = new RootCommand("A command line driven RPG experience")
        {
            RootOptions.StartOption,
            CommonOptions.QuitOption
        };

        var inGameCommand = host.Services.GetService<InGameCommand>();
        var createCharacterCommand = host.Services.GetService<CreateCharacterCommand>();

        rootCommand.AddCommand(inGameCommand!);
        rootCommand.AddCommand(createCharacterCommand!);
        rootCommand.SetHandler((bool startGame, bool endGame) =>
        {
            if (endGame)
            {
                Environment.Exit(1);
            }

            if (startGame)
            {
                Console.WriteLine(AsciArt.Title);
                game = new();
                gameStarted = true;

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
                    game = host.Services.GetService<GameSession>();
                }

                textWriter?.WriteLine($"Welcome to Sick Place {game.CurrentPlayer.Name}.");
                inGameCommand?.InvokeAsync("-h");
            }

            if (gameStarted)
            {
                while (gameStarted)
                {
                    var command = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(command))
                    {
                        inGameCommand!.InvokeAsync("-h");
                        continue;
                    }

                    inGameCommand!.InvokeAsync(command);
                }
            }
        }, RootOptions.StartOption, CommonOptions.QuitOption);

        return rootCommand;
    }
}
