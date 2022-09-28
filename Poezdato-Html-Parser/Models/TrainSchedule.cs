namespace Poezdato_Html_Parser.Models;

public class TrainSchedule {
    public List<RailTimeRoute> Rails { get; set; }

    private readonly string _fromStation;
    private readonly string _toStation;

    public TrainSchedule(string fromStation, string toStation) {
        _fromStation = fromStation;
        _toStation = toStation;
        Rails = new List<RailTimeRoute>();
    }

    public void Add(RailTimeRoute route) {
        Rails.Add(route);
    }

    public string GetHeader(string columnSeparator = "") {
        Console.WriteLine($"\tПоезда от стации - {_fromStation} -> {_toStation}");
        Console.WriteLine(columnSeparator);

        return String.Format("{0, 10} | {1, 10} -> {2, 10} | {3, 5} -> {4, 5} | {5, 5}",
            "Номер", "Откуда", "Куда", "Отпр", "Приб", "Длит") + '\n';
    }

    public string ToString(string columnSeparator = "") {
        string val = "";
        foreach (var rail in Rails) {
            val += String.Format("{0, 10} | {1, 10} -> {2, 10} | {3, 5} -> {4, 5} | {5, 5}",
                rail.Number, rail.StationBegin, rail.StationEnd, rail.Begin, rail.End, rail.Duration) + '\n';
            val += columnSeparator + '\n';
        }

        return val;
    }
}