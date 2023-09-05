using Integration.Domain;
using Integration.Twitch.Domain;
using Integrations;

namespace Integration.Twitch; 

public class TwitchIntegration : IIntegration {
    public async Task<IEnumerable<Manifests>> GetManifests(
        string videoId
    ) {

        IEnumerable<Manifests> streamInfo = await GetStreamInfoAsync(videoId);
        return streamInfo;
    }

    private async Task<IEnumerable<Manifests>> GetStreamInfoAsync(
        string videoId
    ) {
        ITwitchController controller = new TwitchController();
        TwitchVideoIdResponse? content = await controller.GetVideoId( videoId );
        TwitchVideoTokenResponse? tokenResponse = await controller.GetVideoToken( videoId, "kimne78kx3ncx6brgo4mv6wki5h1ko" );
        string videoSourceResponse = await controller.GetVideoSource(
            videoId: videoId,
            token: tokenResponse?.Data.VideoPlaybackAccessToken.Value ?? "",
            signature: tokenResponse?.Data.VideoPlaybackAccessToken.Signature ?? ""
        );
        
        return new List<Manifests>() {
            new (
                new StreamInfo(
                    Url: videoSourceResponse,
                    ThumbnailUrl: "",
                    LengthInSeconds: TimeSpan.Zero,
                    Codec: "",
                    Label: "",
                    Framerate: 30,
                    IsHighDefinition: true,
                    SizeInMb: 100,
                    Filename: content?.Data?.Video.Title ?? ""
                )
            )
        };
    }
}