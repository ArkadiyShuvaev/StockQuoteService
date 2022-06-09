public interface IStockQuoteProvider
{
    public Task<MarketStackSymbolData> Get(string indexSymbol);
}