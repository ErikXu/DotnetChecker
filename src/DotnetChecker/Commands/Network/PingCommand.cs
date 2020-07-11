using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Network
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ping?view=netcore-3.1
    /// </summary>
    [Command("ping", Description = "Ping an address")]
    public class PingCommand
    {
        [Argument(0)]
        [Required]
        public string Address { get; set; }

        public void OnExecute(IConsole console)
        {
            var ping = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };

            var buffer = Encoding.UTF8.GetBytes("12345678123456781234567812345678");
            var timeout = 128;
            var reply = ping.Send(Address, timeout, buffer, options);

            if (reply == null)
            {
                console.WriteLine("Error occurs.");
                return;
            }

            if (reply.Status == IPStatus.Success)
            {
                console.WriteLine($"       Address: {reply.Address}");
                console.WriteLine($"RoundTrip time: {reply.RoundtripTime}");
                console.WriteLine($"  Time to live: {reply.Options.Ttl}");
                console.WriteLine($"Don't fragment: {reply.Options.DontFragment}");
                console.WriteLine($"   Buffer size: {reply.Buffer.Length}");
            }
            else
            {
                console.WriteLine($"Can not reach {Address}, reason: {reply.Status.ToString()}");
            }
        }
    }
}