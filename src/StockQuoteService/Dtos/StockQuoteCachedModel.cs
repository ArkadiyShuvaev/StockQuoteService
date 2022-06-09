using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StockQuoteCachedModel
{
    public string Symbol { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime ExpirationTime { get; set; }

    public decimal Value { get; set; }
}