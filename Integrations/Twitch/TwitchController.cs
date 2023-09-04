using System.Net.Http.Json;
using System.Text;
using Integration.Twitch.Domain;

namespace Integration.Twitch; 

internal class TwitchController : ITwitchController {

    private const string CLIENT_ID = "kimne78kx3ncx6brgo4mv6wki5h1ko";
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
            name: "Client-ID", value: CLIENT_ID   
        );

        
        using HttpResponseMessage response = await m_httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TwitchVideoIdResponse>();
    }

    async Task<TwitchVideoTokenResponse?> ITwitchController.GetVideoToken(
        string videoId,
        string authToken
    ) {
        HttpRequestMessage request = new(
            method: HttpMethod.Post,
            requestUri: "https://gql.twitch.tv/gql"    
        );
        
        request.Content = new StringContent(
            // lang=json
            content: "{\"operationName\":\"PlaybackAccessToken_Template\",\"query\":\"query PlaybackAccessToken_Template($login: String!, $isLive: Boolean!, $vodID: ID!, $isVod: Boolean!, $playerType: String!) {  streamPlaybackAccessToken(channelName: $login, params: {platform: \\\"web\\\", playerBackend: \\\"mediaplayer\\\", playerType: $playerType}) @include(if: $isLive) {    value    signature    __typename  }  videoPlaybackAccessToken(id: $vodID, params: {platform: \\\"web\\\", playerBackend: \\\"mediaplayer\\\", playerType: $playerType}) @include(if: $isVod) {    value    signature    __typename  }}\",\"variables\":{\"isLive\":false,\"login\":\"\",\"isVod\":true,\"vodID\":\"" + videoId + "\",\"playerType\":\"embed\"}}",
            encoding: Encoding.UTF8, 
            mediaType: "application/json"
        );
        
        request.Headers.Add( 
            name: "Client-ID", value: CLIENT_ID   
        );
        
        using HttpResponseMessage response = await m_httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<TwitchVideoTokenResponse>();
    }

    async Task<string> ITwitchController.GetVideoSource(
        string videoId,
        string token,
        string signature
    ) {
        HttpRequestMessage request = new(
            method: HttpMethod.Get,
            requestUri: $"https://usher.ttvnw.net/vod/{videoId}.m3u8?acmb=e30%3D&allow_source=true&p=2747486&play_session_id=7ee0046ba1c3470a79390df560099b08&player_backend=mediaplayer&playlist_include_framerate=true&reassignments_supported=true&sig={signature}&supported_codecs=avc1&token={token}&transcode_mode=vbr_v2&cdm=wv&player_version=1.21.0"    
        );
        
        request.Headers.Add( 
            name: "Client-ID", value: CLIENT_ID   
        );
        
        using HttpResponseMessage response = await m_httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        string playlist = await response.Content.ReadAsStringAsync();

        return playlist;
    }
}