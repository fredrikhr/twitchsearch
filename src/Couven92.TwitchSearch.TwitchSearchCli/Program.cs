using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Polly;

using THNETII.CommandLine.Hosting;
using THNETII.Common;

namespace Couven92.TwitchSearch.TwitchSearchCli
{
    public static partial class Program
    {
        public static Task<int> Main(string[] args)
        {
            return new CommandLineBuilder(
                new RootCommand(GetDescription()))
                .UseDefaults()
                .UseHost(Host.CreateDefaultBuilder, ConfigureHost)
                .Build().InvokeAsync(args);
        }

        private static void ConfigureHost(IHostBuilder host) => host
            .ConfigureHostConfiguration(config =>
            {
                config.AddUserSecrets(typeof(Program).Assembly, optional: true, reloadOnChange: true);
            })
            .ConfigureServices(services =>
            {
                services.AddHttpClient("twitch")
                    .ConfigureHttpClient((serviceProvider, httpClient) =>
                    {
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        if (configuration.GetValue<string>("Twitch:ClientId").TryNotNullOrWhiteSpace(out string clientId))
                        {
                            httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
                        }
                    })
                    .AddPolicyHandler(Policy
                        .HandleResult<HttpResponseMessage>(msg => (int)msg.StatusCode == 429)
                        .WaitAndRetryForeverAsync(
                            sleepDurationProvider: (i, error, ctx) =>
                            {
                                var msg = error.Result;
                                TimeSpan? resetTimespan = null;
                                if (msg.Headers.Date.HasValue &&
                                    msg.Headers.TryGetValues("Ratelimit-Reset", out var ratelimitResetHeaderValues) &&
                                    ratelimitResetHeaderValues.FirstOrDefault().TryNotNullOrEmpty(out string ratelimitResetString) &&
                                    int.TryParse(ratelimitResetString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int ratelimitResetEpochOffsetSeconds))
                                {
                                    var epoch = new DateTimeOffset(1970, 01, 01, 00, 00, 00, TimeSpan.Zero);
                                    var resetTimestamp = epoch + TimeSpan.FromSeconds(ratelimitResetEpochOffsetSeconds);
                                    resetTimespan = resetTimestamp - msg.Headers.Date.Value;
                                }

                                return resetTimespan ?? TimeSpan.FromSeconds(10);
                            },
                            onRetryAsync: (msg, ts, i, ctx) => Task.CompletedTask)
                    );
            });
    }
}
