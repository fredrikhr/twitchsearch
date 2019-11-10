using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Authorization;

namespace Couven92.TwitchSearch.BlazorWasm
{
    public class TwitchAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpClientFactory httpFactory;

        public TwitchAuthStateProvider(IHttpClientFactory httpFactory)
        {
            this.httpFactory = httpFactory;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            using var httpClient = httpFactory.CreateClient(nameof(TwitchClient));
            return null;
        }
    }
}
