using System.Text.Json;

namespace LunchManager.Camco;

public class CamcoHttpRequest {
    private readonly string _url = "http://www.binna.kro.kr";
    public async Task<List<string>> RequestAsync() {
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(_url);
        response.EnsureSuccessStatusCode(); // 상태 코드 체크 (200 OK 확인)

        string responseBody = await response.Content.ReadAsStringAsync();
        // List<string> result = JsonConvert.DeserializeObject<List<string>>(responseBody);

        List<string> result = JsonSerializer.Deserialize<List<string>>(responseBody);

        return result;
    }
}