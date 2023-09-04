using Integration.Twitch.Domain;
using Integrations;
using Integrations.Domain;

namespace Integration.Twitch; 

public class TwitchClient : IDomainClient {
    public async Task<IEnumerable<Manifests>> GetManifests(
        VideoId videoId
    ) {

        IEnumerable<Manifests> streamInfo = await GetStreamInfoAsync(videoId);
        return streamInfo;
    }

    private async Task<IEnumerable<Manifests>> GetStreamInfoAsync(
        VideoId videoId
    ) {
        ITwitchController controller = new TwitchController();
        TwitchVideoIdResponse? content = await controller.GetVideoId( videoId );
        
        return new List<Manifests>() {
            new (
                new StreamInfo(
                    content?.Data.Video.Title
                )
            )
        };
    }
}