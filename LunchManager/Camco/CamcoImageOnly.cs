using HtmlAgilityPack;

namespace LunchManager.Camco;

/// <summary>
/// Get only the image url of the menu
/// use the method <code>
/// // run as async
/// GetInfo();
/// </code>
/// </summary>
public class CamcoImageOnly {
    private readonly string _url = "https://www.bobful.com/bbs/cook_list.php?bo_table=cook&cook_id=24";
    /// <summary>
    /// Get the image url of the menu
    /// </summary>
    /// <returns>Image Url of the menu</returns>
    public async Task<string> GetMenuTableUrl() {
        using (HttpClient client = new()) {
            try {
                string htmlSource = await client.GetStringAsync(_url);

                return GetImageUrl(GetUrlSource(htmlSource));
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        return string.Empty;    // return empty string if failed to get the image url
    }

    private string GetUrlSource(string src) {
        HtmlDocument doc = new();
        doc.LoadHtml(src);

        HtmlNode aTag = doc.DocumentNode.SelectSingleNode("//a[@class='view_image']");
        return aTag.GetAttributeValue("href", string.Empty);
    }

    private string GetImageUrl(string src) {
        string fixedPrefix = "https://www.bobful.com/data/file/cook/";
        string baseprefix = "https://www.bobful.com/bbs/view_image.php?fn=%2Fdata%2Ffile%2Fcook%2F";

        return src.Replace(baseprefix, fixedPrefix);
    }
}
