using System.Text.Json;

public class MarketStackProvider : IStockQuoteProvider
{
    private readonly HttpMessageHandler _handler;
    private readonly ILogger<MarketStackProvider> _logger;
    private readonly StockQuoteSettings _settings;

    public MarketStackProvider(HttpMessageHandler handler,
                              ILogger<MarketStackProvider> logger,
                              StockQuoteSettings settings)
    {
        _handler = handler;
        _logger = logger;
        _settings = settings;
    }
    public async Task<MarketStackSymbolData> Get(string indexSymbol)
    {
        using var client = new HttpClient(_handler, false);
        var response = await client.GetAsync(GetUri(indexSymbol));
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        _logger.LogDebug("The response body: {ResponseBody}", responseBody);

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new MarketStackDateTimeConvertor());
        var marketStackResponse = JsonSerializer.Deserialize<MarketStackResponse>(
            responseBody, options);

        if (marketStackResponse?.Data?[0] == null)
        {
            _logger.LogWarning("The symbol {Symbol} cannot be found on the remote server.", _settings.Symbol);
        }

        return marketStackResponse.Data[0];
    }

    private string GetUri(string symbol)
    {
        return $"{_settings.ApiUri}?access_key={_settings.AccessKey}&symbols={symbol}";
    }
}