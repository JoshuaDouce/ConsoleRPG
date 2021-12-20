// See https://aka.ms/new-console-template for more information
using ConsoleRPG;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<ITextWriter, ConsoleRPG.TextWriter>();
        services.AddOptions<GameSettings>()
            .Bind(config.GetSection("GameSettings"));
    })
    .Build();

await host.StartAsync();

Console.WriteLine(AsciArt.Title);

Game? game = new(textWriter: host.Services.GetService<ITextWriter>());
game.Start();
