using System.Net.Http.Json;
using System.Text;
using Integration.Twitch.Domain;
using Integrations.Domain;

namespace Integration.Twitch; 

internal class TwitchController : ITwitchController {

    private readonly HttpClient m_httpClient = new(){ Timeout = TimeSpan.FromSeconds(30) };

    public async Task<TwitchVideoIdResponse?> GetVideoId(
        string videoId
    ) {
        
        using HttpRequestMessage request = new (
            method: HttpMethod.Post,
            requestUri: "https://gql.twitch.tv/gql"
        );
        request.Content = new StringContent(
            // lang=json
            content: "{\"query\":\"query{video(id:\\\"" + videoId + "\\\"){title,thumbnailURLs(height:180,width:320),createdAt,lengthSeconds,owner{id,displayName},viewCount,game{id,displayName}}}\",\"variables\":{}}",
            encoding: Encoding.UTF8, 
            mediaType: "application/json"
        );

        request.Headers.Add( 
            "Client-ID", "kimne78kx3ncx6brgo4mv6wki5h1ko"   
        );

        
        using HttpResponseMessage response = await m_httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TwitchVideoIdResponse>();
    }

    public async Task<TwitchVideoIdResponse?> GetVideoToken(
        string url,
        string authToken
    ) {
        HttpRequestMessage request = new(
            method: HttpMethod.Post,
            requestUri: "https://gql.twitch.tv/gql"    
        );
        
        request.Content = new StringContent(
            // lang=json
            content: $$$$"""{"operationName":"PlaybackAccessToken_Template","query":"query PlaybackAccessToken_Template($login: String!, $isLive: Boolean!, $vodID: ID!, $isVod: Boolean!, $playerType: String!) {  streamPlaybackAccessToken(channelName: $login, params: {platform: "web", playerBackend: "mediaplayer", playerType: $playerType}) @include(if: $isLive) {    value    signature    __typename  }  videoPlaybackAccessToken(id: $vodID, params: {platform: \"web\", playerBackend: \"mediaplayer\", playerType: $playerType}) @include(if: $isVod) {    value    signature    __typename  }}","variables":{"isLive":false,"login":"","isVod":true,"vodID":"1913372485","playerType":"embed"}}"""
        );
        
        using HttpResponseMessage response = await m_httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TwitchVideoIdResponse>();
    }
}