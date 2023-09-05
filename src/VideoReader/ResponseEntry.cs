namespace VideoReader;

public sealed record VideoQuality(
    string Label,
    int Framerate,
    bool IsHighDefinition
);

public sealed record ResponseEntry ( 
    string Url,
    string Codec,
    VideoQuality VideoQuality,
    double SizeInMb,
    string ThumbnailUrl,
    string Filename
);