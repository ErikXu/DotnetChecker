using System;
using McMaster.Extensions.CommandLineUtils;
using RabbitMQ.Client;

namespace DotnetChecker.Commands.Rabbit
{
    [Command("rabbit", Description = "Check rabbitmq")]
    public class RabbitCommand
    {
        [Option("-u|--uri", "Rabbit uri, example: amqp://user:password@localhost:5672/vhost", CommandOptionType.SingleValue)]
        public string Uri { get; set; }

        [Option("-q|--queue", "Rabbit queue name", CommandOptionType.SingleValue)]
        public string Queue { get; set; }

        public void OnExecute(IConsole console)
        {
            if (string.IsNullOrWhiteSpace(Uri))
            {
                console.WriteLine("The uri can not be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Queue))
            {
                console.WriteLine("The queue name can not be null.");
                return;
            }

            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(Uri)
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                var count = channel.MessageCount(Queue);

                console.WriteLine($"The message count of {Queue} is {count}.");
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}