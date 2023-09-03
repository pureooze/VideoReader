using Integration.Twitch.Domain;
using Integrations.Domain;

namespace Integration; 

public interface ITwitchController {
    Task<TwitchVideoIdResponse?> GetVideoId(
        VideoId url
    );

    Task<TwitchVideoIdResponse?> GetVideoToken(
        VideoId url,
        string authToken
    );
}