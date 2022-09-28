using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Poezdato_Html_Parser.Configs;

public class InformationGetterFromJson {
    private readonly string _fileName;

    /// <summary>
    /// Get values from config
    /// </summary>
    /// <param name="key">Key from settings</param>
    /// <returns>Values of the config file</returns>
    /// <exception cref="NullReferenceException"></exception>
    public virtual string? this[string key] {
        get {

            using var reader = new StreamReader(_fileName);

            JObject data = (JObject)(JsonConvert.DeserializeObject(reader.ReadToEnd()) ?? String.Empty);

            return data[key]?.Value<string>() ?? throw new NullReferenceException($"In {_fileName} value {key} has null value!");

            //return (data.SelectToken(key) ?? 
            //        throw new NullReferenceException($"{_fileName} does not have this property!"))
            //       .Value<string>() 
            //       ?? throw new NullReferenceException($"In {_fileName} value {key} has null value!");

        }
    }

    public virtual bool HasKey(string key) {
        using var reader = new StreamReader(_fileName);

        var data = (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd())!;

        return data.ContainsKey(key);
    }

    public InformationGetterFromJson(string fileName) {
        _fileName = fileName;
    }
}