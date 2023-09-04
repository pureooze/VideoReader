using Integration.Domain;
using Integration.Twitch.Domain;
using Integrations;
using Integrations.Domain;

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
        
        Console.WriteLine("Content: ");
        Console.WriteLine(content);
        return new List<Manifests>() {
            new (
                new StreamInfo(
                    Url: content?.Data.Video.Title,
                    ThumbnailUrl: "",
                    LengthInSeconds: TimeSpan.Zero,
                    Codec: "",
                    Label: "",
                    Framerate: 30,
                    IsHighDefinition: true,
                    SizeInMb: 100
                )
            )
        };
    }
}