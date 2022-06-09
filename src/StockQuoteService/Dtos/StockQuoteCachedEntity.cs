using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("StockQuote")]
public class StockQuoteCachedEntity
{
    [DynamoDBHashKey]
    public string PK { get; set; }

    [DynamoDBRangeKey]
    public string SK { get; set; }

    public long ExpirationTime { get; set; }

    public string Value { get; set; }
}