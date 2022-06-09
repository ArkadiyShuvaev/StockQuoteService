using System.Text.Json;

public class StockQuoteService : IStockQuoteService
{
    private readonly IStockQuoteProvider _stockQuoteProvider;
    private readonly IDynamoDbService _dynamoDbService;
    private readonly ILogger<StockQuoteService> _logger;
    private readonly StockQuoteSettings _settings;

    public StockQuoteService(IStockQuoteProvider stockQuoteProvider,
                            IDynamoDbService dynamoDbService,
                            ILogger<StockQuoteService> logger,
                            StockQuoteSettings settings)
    {
        _stockQuoteProvider = stockQuoteProvider;
        _dynamoDbService = dynamoDbService;
        _logger = logger;
        _settings = settings;
    }
    public async Task<decimal> Get()
    {
        var cachedItem = await _dynamoDbService.Get(_settings.Symbol);
        if (cachedItem != null)
        {
            return cachedItem.Value;
        }

        var item = await _stockQuoteProvider.Get(_settings.Symbol);
        if (item == null)
        {
            throw new Exception($"The symbol '{_settings.Symbol}' cannot be found neither in cache or on remote API.");
        }

        var newCachedItem = new StockQuoteCachedModel
        {
            CreationTime = DateTime.Now,
            ExpirationTime = DateTime.Now.AddDays(14),
            Symbol = _settings.Symbol,
            Value = item.Close
        };

        await _dynamoDbService.Save(newCachedItem);

        return item.Close;
    }
}