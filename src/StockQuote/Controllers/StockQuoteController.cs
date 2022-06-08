using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StockQuoteController : ControllerBase
{
    private readonly ILogger<StockQuoteController> _logger;
    private readonly IStockQuoteService _provider;

    public StockQuoteController(IStockQuoteService provider,
                                ILogger<StockQuoteController> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    public async Task<decimal> Get()
    {
        _logger.LogDebug($"Requesting price for the stock index.");
        return await _provider.Get();
    }
}
