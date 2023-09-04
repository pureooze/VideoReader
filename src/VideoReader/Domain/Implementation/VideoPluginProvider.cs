using System.Collections.Generic;
using VideoReader.Domain.Implementation.Plugins;

namespace VideoReader.Domain.Implementation;

internal class VideoPluginProvider : IVideoPluginProvider
{
    private readonly IEnumerable<IVideoPlugin> m_plugins = new List<IVideoPlugin>() {
        new YoutubePlugin(),
        new TwitchPlugin()
    };

    IEnumerable<IVideoPlugin> IVideoPluginProvider.GetPlugins() {
        return m_plugins;
    }
}