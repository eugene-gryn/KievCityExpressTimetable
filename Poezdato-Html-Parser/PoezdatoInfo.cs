using Poezdato_Html_Parser.Configs;

namespace Poezdato_Html_Parser;

public class PoezdatoInfo {
    public PoezdatoInfo(string poezdatoWebInfo, string stationLinkList, string xPathSelectors) {
        WebInfo = new InformationGetterFromJson($"{Environment.CurrentDirectory}/{poezdatoWebInfo}");
        StationsNamesLinkList = new InformationGetterFromJson($"{Environment.CurrentDirectory}/{stationLinkList}");
        XPathSelectors = new InformationGetterFromJson($"{Environment.CurrentDirectory}/{xPathSelectors}");
    }

    public InformationGetterFromJson WebInfo { get; }
    public InformationGetterFromJson StationsNamesLinkList { get; }
    public InformationGetterFromJson XPathSelectors { get; }

    public string GetHost() {
        return WebInfo["Host"] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public string GetTimetableLink() {
        return WebInfo["TimetableLink"] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public string GetTimeFormat() {
        return WebInfo["TimeFormat"] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public string GetDateFormat() {
        return WebInfo["DateTimeFormat"] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public string GetStationUrl(string name) {
        return StationsNamesLinkList[name] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public bool IsStationOfElectricityTrain(string name) {
        return StationsNamesLinkList.HasKey(name);
    }

    private string SelectXPath(string attribute) {
        return XPathSelectors[attribute] ?? throw new NullReferenceException("Attribute does not found!");
    }

    public string GetNumbersSelector() {
        return SelectXPath("Numbers");
    }

    public string GetFromStationsSelector() {
        return SelectXPath("FromStations");
    }

    public string GetToStationsSelector() {
        return SelectXPath("ToStations");
    }

    public string GetBeginsSelector() {
        return SelectXPath("Begins");
    }

    public string GetEndsSelector() {
        return SelectXPath("Ends");
    }
}