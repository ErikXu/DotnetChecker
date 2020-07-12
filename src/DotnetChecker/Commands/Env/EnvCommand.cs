using System;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Env
{
    [Command("env", Description = "Check environment variables"),
     Subcommand(typeof(EnvGetCommand))]
    public class EnvCommand
    {
        public void OnExecute(IConsole console)
        {
            var envVars = Environment.GetEnvironmentVariables();

            var max = 0;
            foreach (var envVarKey in envVars.Keys)
            {
                var length = envVarKey.ToString().Length;
                if (length > max)
                {
                    max = length;
                }
            }

            foreach (var envVarKey in envVars.Keys)
            {
                console.WriteLine($"{envVarKey.ToString().PadLeft(max, ' ')}: {envVars[envVarKey]}");
            }
        }
    }
}