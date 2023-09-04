namespace Integration.Twitch.Domain; 

public record TwitchVideoTokenResponse(
    TwitchVideoTokenResponseData Data
);

public record TwitchVideoTokenResponseData(
    TwitchVideoToken VideoPlaybackAccessToken
);

public record TwitchVideoToken(
    string Value,
    string Signature
);