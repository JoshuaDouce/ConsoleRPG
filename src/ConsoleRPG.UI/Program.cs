namespace ConsoleRPG.UI;

using System.CommandLine;
using ConsoleRPG;
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
                services.AddTransient<MoveCommand>();
                services.AddTransient<InGameCommand>();
                services.AddOptions<GameSettings>()
                    .Bind(config.GetSection("GameSettings"));
            })
            .Build();

        await host.StartAsync();
        var rootCommand = SetupCommandLine(host);

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

        rootCommand.AddCommand(inGameCommand!);
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
                textWriter?.WriteLine($"Welcome to Sick Place {game.CurrentPlayer.Name}.");
                //TODO: Add character creation
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
