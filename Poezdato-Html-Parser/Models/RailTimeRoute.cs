namespace Poezdato_Html_Parser.Models;

public class RailTimeRoute {
    public RailTimeRoute(string number, string stationBegin, string stationEnd, string begin, string end) {
        Number = number.TrainFormat();
        StationBegin = stationBegin.TrainFormat();
        StationEnd = stationEnd.TrainFormat();

        var beginTime = begin.TrainFormat().Split('.');
        var endTime = end.TrainFormat().Split('.');

        Begin = new TimeOnly(int.Parse(beginTime[0]), int.Parse(beginTime[1]));
        End = new TimeOnly(int.Parse(endTime[0]), int.Parse(endTime[1]));
    }
    public string Number { get; }
    public string StationBegin { get; }
    public string StationEnd { get; }
    public TimeOnly Begin { get; }
    public TimeOnly End { get; }
    public TimeSpan Duration => End - Begin;
}