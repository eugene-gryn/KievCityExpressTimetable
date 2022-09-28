using Poezdato_Html_Parser.Models;

namespace Poezdato_Html_Parser;

public interface IPoezdatoHtmlParser {
    public TrainSchedule? Schedule { get; }

    Task<List<string>> StationStartsWith(string name);
    void RailTimeRoutes(string urlStation, string urlDestination, DateTime date, TimeOnly from, TimeOnly to);
}