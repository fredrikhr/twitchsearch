using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Couven92.TwitchSearch.VideoSearchSelectorCli
{
    public class VideoSearchSelectorApp : BackgroundService
    {
        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }

        public VideoSearchSelectorApp(IConfiguration configuration, ILogger<VideoSearchSelectorApp> logger)
        {
            Configuration = configuration ?? new ConfigurationBuilder().AddInMemoryCollection().Build();
            Logger = (ILogger)logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
