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

        [Option("-d|--driver", "Redis driver, " + StackExchange + " or " + CsRedis + ", default " + StackExchange, CommandOptionType.SingleOrNoValue)]
        public string Type { get; set; }

        [Option("-c|--connection", "Redis connection string, example localhost:6379", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        [Option("-i|--index", "Redis database index, default 0", CommandOptionType.SingleValue)]
        public int Index { get; set; }

        [Argument(0)]
        [Required]
        public string Key { get; set; }

        private readonly IConsole _console;

        public GetStringCommand(IConsole console)
        {
            _console = console;
        }

        public void OnExecute()
        {
            if (string.IsNullOrWhiteSpace(Connection))
            {
                _console.WriteLine("The connection can not be null.");
                return;
            }

            try
            {
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
            catch (Exception ex)
            {
                _console.WriteLine(ex.Message);
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