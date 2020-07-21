using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Info
{
    [Command("info", Description = "Check server info")]
    public class InfoCommand
    {
        private readonly IConsole _console;

        public InfoCommand(IConsole console)
        {
            _console = console;

        }
        public void OnExecute()
        {
            PrintBasicInfo();
            PrintMemory();
            PrintAddress();
        }

        private void PrintBasicInfo()
        {
            _console.WriteLine($"Machine Name: {Environment.MachineName}");
            _console.WriteLine($"Host Name: {Dns.GetHostName()}");
            _console.WriteLine($"User Domain Name: {Environment.UserDomainName}");
            _console.WriteLine($"User Name: {Environment.UserName}");
            _console.WriteLine($"OS Description: {RuntimeInformation.OSDescription}");
            _console.WriteLine($"OS Architecture: {RuntimeInformation.OSArchitecture}");
            _console.WriteLine($"Process Architecture: {RuntimeInformation.ProcessArchitecture}");
            _console.WriteLine($"System PageSize: {Environment.SystemPageSize}");
        }

        private void PrintMemory()
        {
            _console.WriteLine($"Processors: {Environment.ProcessorCount}");

            var info = new ProcessStartInfo
            {
                RedirectStandardOutput = true
            };

            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            if (isUnix)
            {
                info.FileName = "/bin/bash";
                info.Arguments = "-c \"cat /proc/meminfo\"";

                using var process = Process.Start(info);
                if (process != null)
                {
                    var output = process.StandardOutput.ReadToEnd();

                    var lines = output.Split("\n");

                    var regex = new Regex("[0-9]+");

                    var totalLine = lines.First(n => n.StartsWith("MemTotal"));
                    var total = Math.Round(double.Parse(regex.Matches(totalLine)[0].Value) / 1024, 0);

                    var freeLine = lines.First(n => n.StartsWith("MemFree"));
                    var free = Math.Round(double.Parse(regex.Matches(freeLine)[0].Value) / 1024, 0);

                    var used = total - free;

                    _console.WriteLine($"Memory: Total {total}(M), Used {used}(M), Free {free}(M)");
                }
            }
            else
            {
                info.FileName = "wmic";
                info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";

                using var process = Process.Start(info);
                if (process != null)
                {
                    var output = process.StandardOutput.ReadToEnd();

                    var lines = output.Trim().Split("\n");
                    var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
                    var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

                    var total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
                    var free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
                    var used = total - free;

                    _console.WriteLine($"Memory: Total {total}(M), Used {used}(M), Free {free}(M)");
                }
            }
        }

        private void PrintAddress()
        {
            var addresses = Dns.GetHostAddresses(string.Empty);

            _console.WriteLine("Ip Addresses:");

            foreach (var address in addresses)
            {
                _console.WriteLine($"  {address}");
            }
        }
    }
}
