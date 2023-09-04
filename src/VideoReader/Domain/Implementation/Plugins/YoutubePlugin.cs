using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain.Implementation.Plugins;

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
        IEnumerable<IVideoStreamInfo> streamInfoList = await GetVideoStreamInfoManifests( source: sourceUri);

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

    async Task<Stream> IVideoPlugin.GetVideoSource(
        string uri
    ) {
        IVideoPlugin videoPlugin = this;
        IEnumerable<IVideoStreamInfo> streamInfo = await GetVideoStreamInfoManifests( source: uri );
        
        Stream stream = await m_client.Videos.Streams.GetAsync( streamInfo.First() );
        
        return stream;

        // return streamInfoList.Select( 
        //     streamInfo => 
        //         new ResponseEntry( 
        //             Url: streamInfo.Url,
        //             Codec: streamInfo.VideoCodec,
        //             VideoQuality: new VideoQuality( 
        //                 Label: streamInfo.VideoQuality.Label, 
        //                 Framerate: streamInfo.VideoQuality.Framerate, 
        //                 IsHighDefinition: streamInfo.VideoQuality.IsHighDefinition 
        //             ),
        //             SizeInMb: streamInfo.Size.MegaBytes
        //         ) 
        // );
    }

    private async Task<IEnumerable<IVideoStreamInfo>> GetVideoStreamInfoManifests(
        string source
    ) {
        StreamManifest manifest = 
            await m_client.Videos.Streams.GetManifestAsync( source );
        
        return manifest
            .GetMuxedStreams()
            .Where( stream => stream.Container == Container.Mp4 );
    }
}