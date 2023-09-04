namespace Integration.Twitch.Domain; 

public record TwitchVideoIdResponse(
    TwitchVideoIdResponseData? Data,
    TwitchVideoIdResponseExtensions? Extensions
);

public record TwitchVideoIdResponseData(
    Video Video
);

public record Video(
    string Title,
    List<string> ThumbnailUrls,
    DateTime CreatedAt,
    int LengthSeconds,
    Owner Owner,
    int ViewCount,
    Game Game
);

public record TwitchVideoIdResponseExtensions(
    long DurationMilliseconds,
    string RequestId
);

public record Game(
    string Id,
    string DisplayName
);

public record Owner(
    string Id,
    string DisplayName
);