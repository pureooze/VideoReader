namespace Integration.Twitch.Domain; 

public record TwitchVideoIdResponse(
    Data Data,
    Extensions Extensions
);

public record Data(
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

public record Extensions(
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