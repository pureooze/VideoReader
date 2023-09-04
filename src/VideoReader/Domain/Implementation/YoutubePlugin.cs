using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain.Implementation;

internal class YoutubePlugin : IVideoPlugin
{
    private readonly YoutubeClient m_client = new ();

    bool IVideoPlugin.CanHandleUri( string sourceUri )
    {
        const string youtubePattern = @"https:\/\/youtu\.be\/\w+";
        return Regex.Match(
            input: sourceUri, 
            pattern: youtubePattern
        ).Success;
    }

    async Task<IEnumerable<ResponseEntry>> IVideoPlugin.GetManifests(
        string sourceUri
    ) {
        StreamManifest manifest = await m_client.Videos.Streams.GetManifestAsync( sourceUri );

        IEnumerable<IVideoStreamInfo> streamInfoList = GetVideoManifests( manifest );

        return streamInfoList.Select( 
            streamInfo => 
                new ResponseEntry( 
                    Url: streamInfo.Url,
                    Codec: streamInfo.VideoCodec,
                    VideoQuality: new VideoQuality( 
                        Label: streamInfo.VideoQuality.Label, 
                        Framerate: streamInfo.VideoQuality.Framerate, 
                        IsHighDefinition: streamInfo.VideoQuality.IsHighDefinition 
                    ),
                    SizeInMb: streamInfo.Size.MegaBytes
                ) 
        );
    }

    // async Task<Stream> IVideoPlugin.GetVideoSource(
    //     string uri
    // ) {
    //     IVideoPlugin videoPlugin = this;
    //     IEnumerable<ResponseEntry> streamInfo = await videoPlugin.GetManifests( sourceUri: uri );
    //     return await m_client.Videos.Streams.GetAsync( streamInfo.First() );
    // }

    private IEnumerable<IVideoStreamInfo> GetVideoManifests(
        StreamManifest manifest
    ) {
        return manifest
            .GetMuxedStreams()
            .Where( stream => stream.Container == Container.Mp4 );
    }
}