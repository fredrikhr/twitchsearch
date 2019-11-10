using Microsoft.AspNetCore.Blazor.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Couven92.TwitchSearch.BlazorWasm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        [SuppressMessage("Style", "IDE0060: Remove unused parameter")]
        [SuppressMessage("Usage", "CA1801: Review unused parameters")]
        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    
                })
                .UseBlazorStartup<Startup>();
    }
}
