using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Integrations;
using Integrations.Domain;
using Integrations.Twitch;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain.Implementation; 

internal class TwitchPlugin : IVideoPlugin {
    bool IVideoPlugin.CanHandleUri(
        string sourceUri
    ) {
        const string twitchPattern = @"https:\/\/.*twitch\.tv\/.*";
        return Regex.Match(
            input: sourceUri, 
            pattern: twitchPattern
        ).Success;
    }

    async Task<IEnumerable<ResponseEntry>> IVideoPlugin.GetManifests(
        string sourceUri
    ) {
        IDomainClient domainClient = new TwitchClient();

        IEnumerable<Manifests> manifests = await domainClient.GetManifests(
            new VideoId( "hello" )
        );

        return manifests.Select(
            manifest => new ResponseEntry(
                Url: manifest.Stream.Url,
                Codec: "",
                VideoQuality: null,
                SizeInMb: 100
            )
        );
    }
}