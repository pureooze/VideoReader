using Integrations.Domain;

namespace Integrations.Twitch; 

public class TwitchClient : IDomainClient {
    public async Task<IEnumerable<Manifests>> GetManifests(
        VideoId videoId
    ) {

        for( int retriesRemaining = 5; retriesRemaining > 0 ; retriesRemaining--) {
            IEnumerable<Manifests> streamInfo = await GetStreamInfoAsync(videoId);
        }
        
        return new List<Manifests>() {
            new(
                new StreamInfo( "hello" )
            )
        };
    }

    private async Task<IEnumerable<Manifests>> GetStreamInfoAsync(
        VideoId videoId
    ) {
        IDomainController controller = new TwitchController();
        throw new NotImplementedException();
    }
}