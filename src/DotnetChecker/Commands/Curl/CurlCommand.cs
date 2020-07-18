using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetChecker.Commands.Curl
{
    [Command("curl", Description = "Access a url")]
    public class CurlCommand
    {
        [Argument(0, Description = "Url, example http://localhost/")]
        [Required]
        public string Url { get; set; }

        [Option("-m|--method", "Http method, example GET, POST, DELETE etc", CommandOptionType.SingleValue)]
        public string Method { get; set; }

        [Option("-b|--body", "Request body", CommandOptionType.SingleValue)]
        public string Body { get; set; }

        [Option("-t|--content-type", "Content type", CommandOptionType.SingleValue)]
        public string ContentType { get; set; }

        public void OnExecute(IConsole console)
        {
            try
            {
                if (!Url.StartsWith("http://") && !Url.StartsWith("https://"))
                {
                    Url = $"http://{Url}";
                }
                Method = (Method ?? string.Empty).ToUpper();
                Body ??= string.Empty;
                ContentType ??= "application/json";

                var message = new HttpRequestMessage
                {
                    RequestUri = new Uri(Url)
                };

                switch (Method)
                {
                    case "POST":
                        message.Method = HttpMethod.Post;
                        message.Content = new StringContent(Body, Encoding.UTF8, ContentType);
                        break;
                    case "PUT":
                        message.Method = HttpMethod.Put;
                        message.Content = new StringContent(Body, Encoding.UTF8, ContentType);
                        break;
                    case "PATCH":
                        message.Method = HttpMethod.Patch;
                        message.Content = new StringContent(Body, Encoding.UTF8, ContentType);
                        break;
                    case "DELETE":
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                var client = new HttpClient();
                var result = client.SendAsync(message).Result;
                var response = result.Content.ReadAsStringAsync().Result;

                console.WriteLine(response);
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}