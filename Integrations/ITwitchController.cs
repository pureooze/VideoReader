using Integration.Twitch.Domain;
using Integrations.Domain;

namespace Integration; 

public interface ITwitchController {
    Task<TwitchVideoIdResponse?> GetVideoId(
        string url
    );

    Task<TwitchVideoIdResponse?> GetVideoToken(
        string url,
        string authToken
    );
}