using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VideoReader.Domain.Implementation; 

internal class Service : IService {
    
    async Task<IEnumerable<ResponseEntry>> IService.GetVideoSourcesForUriAsync(
        string uri
    ) {
        IVideoPluginProvider videoPluginProvider = new VideoPluginProvider();

        IEnumerable<IVideoPlugin> plugins = videoPluginProvider.GetPlugins();
        foreach( IVideoPlugin plugin in plugins ) {
            if( plugin.CanHandleUri( sourceUri: uri ) ) {

                IEnumerable<ResponseEntry> result =
                    await plugin.GetManifests( sourceUri: uri );

                return result;
            }
        }

        return null;
    }
}