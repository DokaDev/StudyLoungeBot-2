using System.Net;
using System.Text.Json;
using AuthorizeKey;
using ScrapNews.Model;

namespace WebNews;

public class NaverNews {
    private string _requestUrl = "https://openapi.naver.com/v1/search/news.json";

    [Obsolete("Obsolete")]
    public async Task<NaverNewsResponse> RequestAsync(string query, int displayCount) {
        _requestUrl = $"{_requestUrl}?query={query}&display={displayCount}&sort=sim";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_requestUrl);
        request.Headers.Add("X-Naver-Client-Id", NaverApi.ClientId);
        request.Headers.Add("X-Naver-Client-Secret", NaverApi.ClientSecret);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string status = response.StatusCode.ToString();

        if(status != "OK")
            throw new HttpRequestException($"Error in request to Naver API. {status}");

        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new(dataStream);
        string responseFromServer = await reader.ReadToEndAsync();

        // debug
        Console.WriteLine(responseFromServer);

        // Json Text -> Object(NaverNewsResponse)
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        NaverNewsResponse newsResponse = JsonSerializer.Deserialize<NaverNewsResponse>(responseFromServer, options) ?? throw new InvalidOperationException();

        return newsResponse;
    }
}