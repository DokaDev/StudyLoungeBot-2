using HtmlAgilityPack;

namespace LunchManager.Camco;

public class CamcoImageOnly {
    private readonly string _url = "https://www.bobful.com/bbs/cook_list.php?bo_table=cook&cook_id=24";
    /// <summary>
    /// Get the image url of the menu
    /// </summary>
    /// <returns>Image Url of the menu</returns>
    public async Task<string> GetInfo() {
        using (HttpClient client = new()) {
            try {
                string htmlSource = await client.GetStringAsync(_url);
                return await GetImageUrl(htmlSource);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        return string.Empty;
    }

    private async Task<string> GetImageUrl(string src) {
        HtmlDocument doc = new();
        doc.LoadHtml(src);

        HtmlNode aTag = doc.DocumentNode.SelectSingleNode("//a[@class='view_image']");
        return aTag.GetAttributeValue("href", string.Empty);
    }
}