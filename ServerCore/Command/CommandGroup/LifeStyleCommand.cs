using System.Net;
using Discord;
using Discord.Interactions;
using LunchManager.Camco;
using ScrapNews.Model;
using WebNews;

namespace ServerCore.Command.CommandGroup;

[SlashCommandGroup("LifeStyle")]
public class LifeStyleCommand : InteractionModuleBase<SocketInteractionContext> {
    [SlashCommand("bob", "양재 캠코 구내식당 메뉴")]
    public async Task GetLunchMenu() {
        CamcoImageOnly camcoImageOnly = new();
        string imgPath = await camcoImageOnly.GetMenuTableUrl();
        imgPath = imgPath.Replace("https://www.bobful.com/bbs/view_image.php?fn=%2Fdata%2Ffile%2Fcook%2F", "https://www.bobful.com/data/file/cook/");

        var embed = new EmbedBuilder()
            .WithTitle("금주 구내식당 메뉴")
            .WithImageUrl(imgPath);

        await RespondAsync(embed: embed.Build());
    }

    [SlashCommand("news", "네이버 뉴스 검색")]
    public async Task GetNaverNews(
        [Summary("keywords", "검색어")] string keywords,
        [Summary("display", "표시할 검색 결과 개수")] int display = 10
        ) {
        NaverNews naverNews = new();
        NaverNewsResponse response = new(); // blank;
        try {
            response = await naverNews.RequestAsync(keywords, display);
        } catch (Exception e) {
            Console.WriteLine(e.Message);
            await RespondAsync("검색 결과를 가져오는 중 오류가 발생했습니다.");
        }

        var embed = new EmbedBuilder()
            .WithTitle($"'{keywords}'에 대한 뉴스 검색 결과")
            .WithColor(Color.Orange)      // Color.Blue
            .WithCurrentTimestamp();    // Current Time

        foreach (var news in response.NaverNews) {
            embed.AddField(field => {
                field.Name = news.Title.Length > 256 ? WebUtility.HtmlDecode(news.Title.Substring(0, 253)) + "..." : news.Title;
                field.Value = news.Link;
                field.IsInline = false;
            });
            // if(embed.Fields.Count == 25) // limit 25 fields
            //     break;
        }


        if(embed.Fields.Count > 0) await RespondAsync(embed: embed.Build());
        else await RespondAsync("검색 결과가 없습니다.");
    }
}