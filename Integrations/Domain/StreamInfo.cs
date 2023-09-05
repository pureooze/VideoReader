using YoutubeExplode.Videos.Streams;

namespace Integration.Domain;

public sealed record StreamInfo(
    string? Url,
    string ThumbnailUrl,
    string Codec,
    TimeSpan? LengthInSeconds,
    string Label,
    int Framerate,
    bool IsHighDefinition,
    double SizeInMb,
    string Filename
);