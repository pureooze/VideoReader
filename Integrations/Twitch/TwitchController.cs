using Integrations.Domain;

namespace Integrations.Twitch; 

internal class TwitchController : IDomainController {
    public bool GetPlayerResponseAsync(
        VideoId videoId
    ) {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.twitch.tv/helix/videos"
        )
        {
            Content = new StringContent(
                // lang=json
                $$"""
                  {
                      "videoId": "{{videoId}}",
                      "context": {
                          "client": {
                              "clientName": "ANDROID_TESTSUITE",
                              "clientVersion": "1.9",
                              "androidSdkVersion": 30,
                              "hl": "en",
                              "gl": "US",
                              "utcOffsetMinutes": 0
                          }
                      }
                  }
                  """
            )
        };
    }
}