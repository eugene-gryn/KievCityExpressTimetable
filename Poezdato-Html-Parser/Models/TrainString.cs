namespace Poezdato_Html_Parser.Models;

public static class TrainString {
    public static string TrainFormat(this string str)
    {
        str = str.Replace(" ", "", StringComparison.Ordinal);
        str = str.Replace("\n", "", StringComparison.Ordinal);
        return str;
    }
}