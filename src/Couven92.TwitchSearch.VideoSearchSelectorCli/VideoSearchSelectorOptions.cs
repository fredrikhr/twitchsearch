using System;
using System.CommandLine;
using System.CommandLine.Builder;

namespace Couven92.TwitchSearch.VideoSearchSelectorCli
{
    public class VideoSearchSelectorOptions
    {
        public VideoSearchSelectorOptions(ParseResult parseResult)
        {

        }

        public bool IsValid()
        {
            return true;
        }
    }

    internal static class VideoSearchSelectorOptionsExtensions
    {
        public static TBuilder AddVideoSearchSelectorOptions<TBuilder>(this TBuilder builder)
            where TBuilder : CommandBuilder
        {
            return builder
                ;
        }
    }
}
