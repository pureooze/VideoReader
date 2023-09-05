using Integration.Domain;
using Integrations;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Integration.Youtube; 

public class YoutubeIntegration : IIntegration {
    
    private readonly YoutubeClient m_integrationClient = new ();
    async Task<IEnumerable<Manifests>> IIntegration.GetManifests(
        string videoId
    ) {
        IEnumerable<IVideoStreamInfo> streamInfoList = 
            await GetVideoStreamInfoManifests( source: videoId);
        IVideo video = await GetVideoInfo( sourceUri: videoId );

        return streamInfoList.Select(
            streamInfo => new Manifests(
                new StreamInfo(
                    Url: streamInfo.Url,
                    ThumbnailUrl: "",
                    LengthInSeconds: video.Duration,
                    Codec: streamInfo.VideoCodec,
                    Label: streamInfo.VideoQuality.Label,
                    Framerate: streamInfo.VideoQuality.Framerate,
                    IsHighDefinition: streamInfo.VideoQuality.IsHighDefinition,
                    SizeInMb: streamInfo.Size.MegaBytes,
                    Filename: streamInfo.Container.Name
                )
            )
        );
    }

    private async Task<IVideo> GetVideoInfo(
        string sourceUri
    ) {
        IVideo video = await m_integrationClient.Videos.GetAsync( videoId: sourceUri );
        return video;
    }

    private async Task<IEnumerable<IVideoStreamInfo>> GetVideoStreamInfoManifests(
        string source
    ) {
        StreamManifest manifest = 
            await m_integrationClient.Videos.Streams.GetManifestAsync( source );
        
        return manifest
            .GetMuxedStreams()
            .Where( stream => 
                stream.Container == Container.Mp4 ||
                stream.Container == Container.WebM
            );
    }
}