using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Couven92.TwitchSearch.VideoSearchSelectorCli
{
    public static partial class Program
    {
        public static Task<int> Main(string[] args)
        {
            Option login;

            login = new Option(nameof(login),
                description: "",
                argument: new Argument<string> { Arity = ArgumentArity.ZeroOrMore }
                );


            var cliRootCommand = new RootCommand(
                description: GetDescription(),
                symbols: new Symbol[] { login },
                handler: CommandHandler.Create<ParseResult, IConsole, BindingContext>((parseResult, console, bindingCtx) =>
                {
                    return Host.CreateDefaultBuilder()
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton(parseResult);
                            services.AddSingleton(console);
                            services.AddSingleton(bindingCtx);

                            services.AddHostedService<VideoSearchSelectorApp>();
                            services.Configure<VideoSearchSelectorOptions>(options =>
                            {
                                var loginValue = parseResult.FindResultFor(login).GetValueOrDefault<string>();
                            });
                        })
                        .RunConsoleAsync();
                }));
            return cliRootCommand.InvokeAsync(args);
        }
    }
}
