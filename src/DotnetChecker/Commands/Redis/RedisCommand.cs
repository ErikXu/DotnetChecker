using System;
using McMaster.Extensions.CommandLineUtils;
using StackExchange.Redis;

namespace DotnetChecker.Commands.Redis
{
    [Command("redis", Description = "Check redis"), 
     Subcommand(typeof(GetStringCommand))]
    public class RedisCommand
    {
        private const string StackExchange = "StackExchange";
        private const string CsRedis = "CsRedis";

        [Option("-d|--Driver", "Redis driver, " + StackExchange + " or " + StackExchange + ", default " + StackExchange, CommandOptionType.SingleOrNoValue)]
        public string Type { get; set; }

        [Option("-c|--Connection", "Redis connection string, example: localhost:6379", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        private readonly IConsole _console;

        public RedisCommand(IConsole console)
        {
            _console = console;
        }

        private void OnExecute()
        {
            if (string.IsNullOrWhiteSpace(Connection))
            {
                _console.WriteLine("The connection can not be null.");
                return;
            }

            switch (Type)
            {
                case CsRedis:
                    break;
                default:
                    try
                    {
                        CheckStackExchange(Connection);
                    }
                    catch (Exception ex)
                    {
                        _console.WriteLine("Error occurs.");
                        _console.WriteLine(ex);
                    }
                   
                    break;
            }
        }

        private void CheckStackExchange(string connection)
        {
            var conn = ConnectionMultiplexer.Connect(connection);

            var db = conn.GetDatabase();

            var key = $"dotnet-checker-{Guid.NewGuid()}";
            db.StringSet(key, "OK");
            _console.WriteLine("StringSet is normal.");

            var value = db.StringGet(key);
            _console.WriteLine($"StringGet is normal, value is {value}.");

            var isOk = db.KeyDelete(key);
            if (isOk)
            {
                _console.WriteLine("KeyDelete is normal.");
            }
        }
    }
}