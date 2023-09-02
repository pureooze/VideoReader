using System.Collections.Generic;

namespace VideoReader.Domain;

public interface IVideoPluginProvider
{
    IEnumerable<IVideoPlugin> GetPlugins();
}