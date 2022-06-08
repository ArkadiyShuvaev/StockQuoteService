using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine($"Running in development.");
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");


app.Run();


static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IStockQuoteService, StockQuoteService>();
    builder.Services.AddScoped<IStockQuoteProvider, MarketStackProvider>();
    builder.Services.AddScoped<HttpMessageHandler>(_ => new HttpClientHandler());

    // Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
    // package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
    builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

    builder.Logging.AddJsonConsole();
    builder.Services.AddSingleton<StockQuoteSettings>(_ =>
        builder.Configuration.GetSection("StockQuoteSettings").Get<StockQuoteSettings>());

    AddAwsDynamoDb(builder);

    builder.Services.AddScoped<IDynamoDbService, DynamoDbService>();
}

static void AddAwsDynamoDb(WebApplicationBuilder builder)
{
    var config = new AmazonDynamoDBConfig()
    {
        RegionEndpoint = RegionEndpoint.EUCentral1
    };
    var client = new AmazonDynamoDBClient(config);
    builder.Services.AddSingleton<IAmazonDynamoDB>(client);
    builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
}