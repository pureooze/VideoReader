using System.Collections.Generic;
using VideoReader.Domain.Implementation.Plugins;

namespace VideoReader.Domain.Implementation;

internal class VideoPluginProvider : IVideoPluginProvider
{
    IEnumerable<IVideoPlugin> IVideoPluginProvider.GetPlugins()
    {
        return new List<IVideoPlugin>()
        {
            new YoutubePlugin(),
            new TwitchPlugin()
        };
    }
}