using System;
using McMaster.Extensions.CommandLineUtils;
using Nest;

namespace DotnetChecker.Commands.ElasticSearch
{
    [Command("es", Description = "Check elastic search")]
    public class ElasticSearchCommand
    {
        [Option("-u|--uri", "Elastic search uri, example: http://localhost:9200", CommandOptionType.SingleValue)]
        public string Uri { get; set; }

        [Option("-i|--index", "Index name", CommandOptionType.SingleValue)]
        public string Index { get; set; }

        public void OnExecute(IConsole console)
        {
            if (string.IsNullOrWhiteSpace(Uri))
            {
                console.WriteLine("The uri can not be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Index))
            {
                console.WriteLine("The index name can not be null.");
                return;
            }

            try
            {
                var node = new Uri(Uri);
                var settings = new ConnectionSettings(node);
                var client = new ElasticClient(settings);

                var isExisted = client.Indices.Exists(Index).Exists;

                if (isExisted)
                {
                    console.WriteLine($"The index {Index} is existed.");
                }
                else
                {
                    console.WriteLine($"The index {Index} is not existed.");
                }
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}