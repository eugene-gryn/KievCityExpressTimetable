using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Poezdato_Html_Parser.HTMLDecoders;

public class WebScheduleClient : IDisposable {
    private bool _disposed;

    public WebScheduleClient(PoezdatoInfo info) {
        var config = new HttpClientHandler {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        Client = new HttpClient(config);

        Client.BaseAddress = new Uri($"https://{info.GetHost()}");

        Client.DefaultRequestHeaders.Add("Accept", "*/*");
        Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        Client.DefaultRequestHeaders.Add("Connection", "keep-alive");
    }

    public HttpClient Client { get; set; }


    public void Dispose() {
        if (!_disposed) Client.Dispose();

        _disposed = true;
    }

    public async Task<List<string>> StationStartsWith(string name) {
        var collectionStations = new List<string>();

        var responce = await Client.GetAsync($"search/get-part-stations?term={name}");


        if (responce.IsSuccessStatusCode) {
            var res = await responce.Content.ReadAsStringAsync();


            var deserializeObject = (JObject) JsonConvert.DeserializeObject(res)!;

            collectionStations = (deserializeObject["response"]
                                  ?? throw new NullReferenceException("Invalid struct of response!"))
                .Select(token => (token.SelectToken("name")
                                  ?? throw new NullReferenceException("Name does not exist in response!"))
                    .Value<string>()).ToList()!;
        }

        return collectionStations;
    }
}