using System.Globalization;
using Amazon.DynamoDBv2.DataModel;

public class DynamoDbService : IDynamoDbService
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly ILogger<DynamoDbService> _logger;

    public DynamoDbService(IDynamoDBContext dynamoDBContext, ILogger<DynamoDbService> logger)
    {
        _dynamoDBContext = dynamoDBContext;
        _logger = logger;
    }

    public async Task<StockQuoteCachedModel> Get(string symbol)
    {
        var stockIndexPartitionKey = GetStockIndexPartitionKey(symbol);
        var result = await _dynamoDBContext
                        .QueryAsync<StockQuoteCachedEntity>(
                            stockIndexPartitionKey, new DynamoDBOperationConfig
                            {
                                BackwardQuery = true
                            })
                        .GetRemainingAsync();

        if (result == null || !result.Any())
        {
            return null;
        }

        var entity = result[0];
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        return new StockQuoteCachedModel
        {
            Symbol = symbol,
            CreationTime = DateTime.Parse(entity.SK, CultureInfo.InvariantCulture),
            ExpirationTime = dateTime.AddSeconds(entity.ExpirationTime).ToLocalTime(),
            Value = decimal.Parse(entity.Value)
        };
    }

    public async Task Save(StockQuoteCachedModel cachedItem)
    {
        var entity = new StockQuoteCachedEntity
        {
            PK = GetStockIndexPartitionKey(cachedItem.Symbol),
            SK = cachedItem.CreationTime.ToString("o", CultureInfo.InvariantCulture),
            ExpirationTime = ((DateTimeOffset)cachedItem.ExpirationTime).ToUnixTimeSeconds(),
            Value = cachedItem.Value.ToString()
        };

        await _dynamoDBContext.SaveAsync<StockQuoteCachedEntity>(entity);
    }

    private static string GetStockIndexPartitionKey(string symbol)
    {
        return $"symbol_{symbol}";
    }
}