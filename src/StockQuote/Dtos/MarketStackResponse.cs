// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

public class MarketStackResponse
{
    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; }

    [JsonPropertyName("data")]
    public List<MarketStackSymbolData> Data { get; set; }
}

public class MarketStackSymbolData
{
    [JsonPropertyName("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("high")]
    public decimal High { get; set; }

    [JsonPropertyName("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("close")]
    public decimal Close { get; set; }

    [JsonPropertyName("volume")]
    public decimal Volume { get; set; }

    [JsonPropertyName("split_factor")]
    public decimal SplitFactor { get; set; }

    [JsonPropertyName("dividend")]
    public decimal Dividend { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("exchange")]
    public string Exchange { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}

public class Pagination
{
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}
