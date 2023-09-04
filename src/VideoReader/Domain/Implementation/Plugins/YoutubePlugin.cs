using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Integration.Domain;
using Integration.Youtube;
using Integrations;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain.Implementation.Plugins;

internal class YoutubePlugin : IVideoPlugin {
    private readonly IIntegration m_integration = new YoutubeIntegration();

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
        IEnumerable<Manifests> manifests = await m_integration.GetManifests( sourceUri );

        return manifests.Select( 
            manifest => 
                new ResponseEntry( 
                    Url: manifest.Stream.Url,
                    Codec: manifest.Stream.Codec,
                    VideoQuality: new VideoQuality( 
                        Label: manifest.Stream.Label, 
                        Framerate: manifest.Stream.Framerate, 
                        IsHighDefinition: manifest.Stream.IsHighDefinition 
                    ),
                    SizeInMb: manifest.Stream.SizeInMb,
                    ThumbnailUrl: null
                ) 
        );
    }
}