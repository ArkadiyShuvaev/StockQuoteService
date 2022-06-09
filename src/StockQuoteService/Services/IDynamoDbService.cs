public interface IDynamoDbService
{
    Task<StockQuoteCachedModel> Get(string symbol);
    Task Save(StockQuoteCachedModel cachedItem);
}
