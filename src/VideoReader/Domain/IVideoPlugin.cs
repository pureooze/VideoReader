using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VideoReader.Domain.Implementation;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain;

public interface IVideoPlugin
{
    bool CanHandleUri( string sourceUri );
    Task<IEnumerable<ResponseEntry>> GetManifests(
        string sourceUri
    );
    // Task<Stream> GetVideoSource(
    //     string uri
    // );
}