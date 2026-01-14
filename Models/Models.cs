using System.Text.Json.Serialization;

namespace BitcoinPriceCheckerDotNet.Models;

public record BitcoinPrice(
    [property: JsonPropertyName("bitcoin")] CoinData Data
);

public record CoinData(
    [property: JsonPropertyName("usd")] decimal UsdPrice
);

public record PriceResponse(string Symbol, string Price, DateTime FetchedAt);
