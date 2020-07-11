using DotnetChecker.Commands.Env;
using DotnetChecker.Commands.Info;
using DotnetChecker.Commands.Mongo;
using DotnetChecker.Commands.Network;
using DotnetChecker.Commands.Redis;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetChecker
{
    [HelpOption(Inherited = true)]
    [Command(Description = "A tool to check .net core capable for redis"),
     Subcommand(typeof(InfoCommand)), Subcommand(typeof(RedisCommand)), 
     Subcommand(typeof(MongoCommand)), Subcommand(typeof(EnvCommand)), 
     Subcommand(typeof(PingCommand)), Subcommand(typeof(TelnetCommand))]
    class Program
    {
        public static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(PhysicalConsole.Singleton);

            var services = serviceCollection.BuildServiceProvider();

            var app = new CommandLineApplication<Program>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            var console = (IConsole)services.GetService(typeof(IConsole));

            try
            {
                return app.Execute(args);
            }
            catch (UnrecognizedCommandParsingException ex)
            {
                console.WriteLine(ex.Message);
                return -1;
            }
        }

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("Please specify a command.");
            app.ShowHelp();
            return 1;
        }
    }
}
