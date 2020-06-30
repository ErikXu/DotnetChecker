using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using StackExchange.Redis;

namespace DotnetChecker.Commands.Redis
{
    [Command("get", Description = "Get redis string value")]
    public class GetStringCommand
    {
        private const string StackExchange = "StackExchange";
        private const string CsRedis = "CsRedis";

        [Option("-d|--Driver", "Redis driver, " + StackExchange + " or " + StackExchange + ", default " + StackExchange, CommandOptionType.SingleOrNoValue)]
        public string Type { get; set; }

        [Option("-c|--Connection", "Redis connection string, example: localhost:6379", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        [Option("-i|--Index", "Redis database index, default 0", CommandOptionType.SingleValue)]
        public int Index { get; set; }

        [Argument(0)]
        [Required]
        public string Key { get; set; }

        private readonly IConsole _console;

        public GetStringCommand(IConsole console)
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

            var db = conn.GetDatabase(Index);

            var value = db.StringGet(Key);
            _console.WriteLine(value);
        }
    }
}