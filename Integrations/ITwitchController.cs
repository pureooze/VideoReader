using Integration.Twitch.Domain;

namespace Integration; 

public interface ITwitchController {
    Task<TwitchVideoIdResponse?> GetVideoId(
        string url
    );

    Task<TwitchVideoTokenResponse?> GetVideoToken(
        string videoId,
        string authToken
    );

    Task<string> GetVideoSource(
        string videoId,
        string token,
        string signature
    );
}