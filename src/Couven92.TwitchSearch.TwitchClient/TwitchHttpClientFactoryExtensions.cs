using Microsoft.Extensions.DependencyInjection;

using Polly;

using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using THNETII.Common;

namespace Couven92.TwitchSearch.TwitchClient
{
    public static class TwitchHttpClientFactoryExtensions
    {
        public static IHttpClientBuilder AddTwitchApiRatelimitHandler(
            this IHttpClientBuilder httpClient)
        {
            httpClient.AddPolicyHandler(Policy
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

            return httpClient;
        }
    }
}
