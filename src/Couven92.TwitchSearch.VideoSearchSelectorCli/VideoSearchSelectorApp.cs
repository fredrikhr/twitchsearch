using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Couven92.TwitchSearch.VideoSearchSelectorCli
{
    public class VideoSearchSelectorApp : BackgroundService
    {
        public VideoSearchSelectorOptions Options { get; }
        public IHttpClientFactory HttpClientFactory { get; }
        public ILogger Logger { get; }

        public VideoSearchSelectorApp(VideoSearchSelectorOptions options, IHttpClientFactory httpClientFactory, ILogger<VideoSearchSelectorApp> logger)
        {
            Options = options;
            HttpClientFactory = httpClientFactory;
            Logger = (ILogger)logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jsonSerializer = new JsonSerializer();
            using (var httpClient = HttpClientFactory.CreateClient("twitch"))
            {
                
            }

            throw new NotImplementedException();
        }
    }
}
