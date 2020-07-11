using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Network
{
    [Command("telnet", Description = "telnet an address and port")]
    public class TelnetCommand
    {
        [Argument(0)]
        [Required]
        public string Address { get; set; }

        [Argument(1)]
        [Required]
        public int Port { get; set; }

        public void OnExecute(IConsole console)
        {
            try
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(Address, Port);

                console.WriteLine($"Connected to {Address}:{Port}");
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}