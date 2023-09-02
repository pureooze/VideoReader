using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace VideoReader.Domain.Implementation; 

public sealed record ResponseEntry ( 
    string Url,
    string Codec,
    VideoQuality VideoQuality,
    double SizeInMb
);