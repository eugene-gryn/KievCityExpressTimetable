namespace Poezdato_Html_Parser.Models;

public class HtmlTrainTable {
    public int Count => Numbers.Count;

    public List<string> Numbers { get; } = new();
    public List<string> FromStations { get; } = new();
    public List<string> ToStations { get; } = new();
    public List<string> Begins { get; } = new();
    public List<string> Ends { get; } = new();
}