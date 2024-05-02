using System.Text.Json;

namespace LunchManager.Camco;

public class CamcoHttpRequest {
    private readonly string _url = "http://www.binna.kro.kr";

    public async Task<List<string>> RequestAsync() {
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(_url);
        response.EnsureSuccessStatusCode(); // 상태 코드 체크 (200 OK 확인)

        string responseBody = await response.Content.ReadAsStringAsync();

        // JSON 파싱 (System.Text.Json 사용)
        JsonDocument jsonDocument = JsonDocument.Parse(responseBody);
        List<string> meals = jsonDocument.RootElement.GetProperty("body").EnumerateArray()
            .Select(element => element.GetString())
            .ToList()!;

        return meals;
    }
}