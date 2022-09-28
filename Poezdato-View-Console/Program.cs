using System.Threading.Channels;
using Poezdato_Html_Parser;
using Poezdato_Html_Parser.Configs;
using Poezdato_Html_Parser.Parsers;



var info = new PoezdatoInfo(
    poezdatoWebInfo: "Poezdato.json",
    stationLinkList: "ElectricTrainStationsLinkNames.json",
    xPathSelectors: "XPath.json");

IPoezdatoHtmlParser parser = new PoezdatoLoader(info);

Stations(out var stationFrom, out var stationTo);

stationFrom = (await parser.StationStartsWith(stationFrom)).Single();
stationTo = (await parser.StationStartsWith(stationTo)).Single();

parser.RailTimeRoutes(stationFrom, stationTo, default, default, default);

Console.WriteLine(parser.Schedule?.GetHeader("---------------------------------------------------------------------"));
Console.WriteLine(parser.Schedule?.ToString("===-----------------------------------------------------------==="));


void Stations(out string stationFrom, out string stationTo) {
    Console.WriteLine("Откуда: ");
    stationFrom = Console.ReadLine()!;
    //var stationFrom = "Левый";
    Console.WriteLine("Куда: ");
    stationTo = Console.ReadLine()!;
    //var stationTo = "Караваевы";
}