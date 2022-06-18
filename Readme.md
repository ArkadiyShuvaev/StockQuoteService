The project returns a stock price for the given stock index. The DynampoDb table is used as a cache to reduce the number of calls to the remote API. The remote API has a limit of 100 requests per month.

# How to deploy
1. Install the .NET Core Global Tool:
    ```cmd
    dotnet tool install -g Amazon.Lambda.Tools
    ```
1. Clone the project
1. Go to the ```src``` folder
1. Execute the command below
    ```cmd
    dotnet lambda deploy-function StockQuoteService --region eu-central-1 --function-role "arn:aws:iam::165819210796:role/service-role/stock-quote-service-role" --function-runtime dotnet6 --function-memory-size 128 --package-type zip --configuration Release --function-timeout 15 --function-handler StockQuote
    ```
1. To debug the Lambda please add to the command above:
    ```cmd
    --environment-variables LAMBDA_NET_SERIALIZER_DEBUG=true;
    ```