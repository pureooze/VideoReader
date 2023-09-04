using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VideoReader.Domain.Implementation;

namespace VideoReader.Domain;

public interface IVideoPlugin
{
    bool CanHandleUri( string sourceUri );

    PluginOutputType GetPluginType();
    
    Task<IEnumerable<ResponseEntry>> GetManifests(
        string sourceUri
    );
}