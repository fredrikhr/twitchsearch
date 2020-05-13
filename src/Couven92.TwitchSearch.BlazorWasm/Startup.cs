using System.Threading.Tasks;
using Couven92.TwitchSearch.TwitchClient;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Couven92.TwitchSearch.BlazorWasm
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddAuthentication()
                .AddTwitch(opts =>
                {
                    opts.Events.OnTicketReceived = ticket =>
                    {
                        ticket.Properties.GetTokens();
                        ticket.HandleResponse();
                        return Task.CompletedTask;
                    };
                });
            services.AddScoped<AuthenticationStateProvider, TwitchAuthStateProvider>();
            services.AddHttpClient(nameof(TwitchClient))
                .AddTwitchApiRatelimitHandler()
                ;
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
