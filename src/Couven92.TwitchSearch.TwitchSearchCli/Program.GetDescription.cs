using System.Reflection;

namespace Couven92.TwitchSearch.TwitchSearchCli
{
    public static partial class Program
    {
        public static string GetDescription()
        {
            return typeof(Program).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description
                ?? typeof(Program).Assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product
                ?? $"{typeof(Program).Assembly.GetName().Name}, v{GetVersionString()}";
        }

        public static string GetVersionString()
        {
            return typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? typeof(Program).Assembly.GetName().Version.ToString();
        }
    }
}
