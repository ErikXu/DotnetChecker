using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Env
{
    [Command("get", Description = "Get environment value of key")]
    public class EnvGetCommand
    {
        [Argument(0)]
        [Required]
        public string Key { get; set; }

        public void OnExecute(IConsole console)
        {
            var value = Environment.GetEnvironmentVariable(Key);
            console.WriteLine($"{Key}: {value}");
        }
    }
}