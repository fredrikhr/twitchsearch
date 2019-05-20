using System;
using System.CommandLine;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Couven92.TwitchSearch.TwitchSearchCli
{
    public class TwitchSearchCliApp : BackgroundService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TwitchSearchCliApp(ParseResult cliArguments,
            IHttpClientFactory httpClientFactory,
            ILogger<TwitchSearchCliApp> logger = null)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            Logger = logger is null ? NullLogger.Instance : (ILogger)logger;
        }

        public ILogger Logger { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
