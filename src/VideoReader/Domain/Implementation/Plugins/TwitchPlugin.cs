using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Integration.Twitch;
using Integrations;
using Integrations.Domain;

namespace VideoReader.Domain.Implementation.Plugins; 

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
        IIntegration integration = new TwitchIntegration();

        int index = sourceUri.LastIndexOf('/');
        IEnumerable<Manifests> manifests = await integration.GetManifests(
            sourceUri[(index+1)..]
        );

        return manifests.Select(
            manifest => new ResponseEntry(
                Url: manifest.Stream.Url,
                Codec: "",
                VideoQuality: null,
                SizeInMb: 100,
                ThumbnailUrl: manifest.Stream.ThumbnailUrl
            )
        );
    }
}