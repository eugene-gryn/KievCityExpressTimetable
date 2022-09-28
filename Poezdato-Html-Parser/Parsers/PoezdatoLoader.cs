using Poezdato_Html_Parser.HTMLDecoders;
using Poezdato_Html_Parser.Models;

namespace Poezdato_Html_Parser.Parsers;

public class PoezdatoLoader : IPoezdatoHtmlParser, IDisposable {
    private readonly WebScheduleClient _client;

    private readonly PoezdatoInfo _info;

    public PoezdatoLoader(PoezdatoInfo info) {
        _info = info;
        _client = new WebScheduleClient(info);
    }

    private HtmlTrainTable? HtmlTable { get; set; }

    public void Dispose() {
        _client.Dispose();
    }

    public TrainSchedule? Schedule { get; private set; }


    /// <summary>
    ///     Returns stations names
    /// </summary>
    /// <param name="name">start of the name station</param>
    /// <returns>List of the names</returns>
    /// <exception cref="NullReferenceException">Invalid response struct</exception>
    public async Task<List<string>> StationStartsWith(string name) {
        var stations = await _client.StationStartsWith(name);

        return stations.Where(s => _info.IsStationOfElectricityTrain(s)).ToList();
    }

    public void RailTimeRoutes(string urlStation, string urlDestination, DateTime date,
        TimeOnly from,
        TimeOnly to) {
        Schedule = new TrainSchedule(urlStation, urlDestination);

        if (date == default) date = DateTime.Today.Date;
        if (from == default) from = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute);
        if (to == default) to = new TimeOnly(23, 59);

        var link =
            $"{_info.GetTimetableLink()}/{_info.GetStationUrl(urlStation)}--{_info.GetStationUrl(urlDestination)}/{date.ToString(_info.GetDateFormat())}/{from.ToString(_info.GetTimeFormat())}/{to.ToString(_info.GetTimeFormat())}/";

        HtmlTable = new HtmlTrainTable();

        HTMLPoezdatoParser.New(_client, _info).GetTables(link, HtmlTable);

        for (var i = 0; i < HtmlTable.Count; i++)
            Schedule.Add(new RailTimeRoute(
                HtmlTable.Numbers[i],
                HtmlTable.FromStations[i],
                HtmlTable.ToStations[i],
                HtmlTable.Begins[i],
                HtmlTable.Ends[i]));
    }
}