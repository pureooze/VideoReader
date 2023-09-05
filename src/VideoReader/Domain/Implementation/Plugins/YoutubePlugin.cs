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

    private readonly string[] m_youtubePatterns = {
        @"https:\/\/youtu\.be\/\w+",
        @"https:\/\/www.youtube.com\/.*"
    };

    bool IVideoPlugin.CanHandleUri( string sourceUri ) {
        return m_youtubePatterns.Any( 
            pattern => 
                Regex.Match( input: sourceUri, pattern: pattern).Success 
        );
    }

    PluginOutputType IVideoPlugin.GetPluginType() {
        return PluginOutputType.File;
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
                    ThumbnailUrl: null,
                    Filename: manifest.Stream.Filename
                ) 
        );
    }
}