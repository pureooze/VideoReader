using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VideoReader.Domain;

public interface IVideoPlugin
{
    bool CanHandleUri( string sourceUri );
    Task<IEnumerable<ResponseEntry>> GetManifests(
        string sourceUri
    );
}