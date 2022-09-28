using AngleSharp;
using AngleSharp.XPath;
using Poezdato_Html_Parser.Models;

namespace Poezdato_Html_Parser.HTMLDecoders;

public class HTMLPoezdatoParser {
    private readonly WebScheduleClient _client;
    private readonly PoezdatoInfo _info;

    public static HTMLPoezdatoParser New(WebScheduleClient client, PoezdatoInfo info) {
        return new HTMLPoezdatoParser(client, info);
    }

    public HTMLPoezdatoParser(WebScheduleClient client, PoezdatoInfo info) {
        _client = client;
        _info = info;
    }

    public void GetTables(string link, HtmlTrainTable trainTable) {

        var config = Configuration.Default.WithDefaultLoader();

        var htmlPage = _client.Client.GetStringAsync(link).Result;


        using var page = BrowsingContext.New(config).OpenAsync(req => req.Content(htmlPage)).Result;

        if (page.Body != null) {
            trainTable.Numbers.AddRange(page.Body.SelectNodes(_info.GetNumbersSelector())
                .Select(n => n.TextContent));
            trainTable.FromStations.AddRange(page.Body.SelectNodes(_info.GetFromStationsSelector())
                .Select(n => n.TextContent));
            trainTable.ToStations.AddRange(page.Body.SelectNodes(_info.GetToStationsSelector())
                .Select(n => n.TextContent));
            trainTable.Begins.AddRange(page.Body.SelectNodes(_info.GetBeginsSelector())
                .Select(n => n.TextContent));
            trainTable.Ends.AddRange(page.Body.SelectNodes(_info.GetEndsSelector())
                .Select(n => n.TextContent));
        }
    }
}