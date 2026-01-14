using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using BitcoinPriceCheckerDotNet.Models;

namespace BitcoinPriceCheckerDotNet.Services;

public class BitcoinService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<BitcoinService> _logger;

    public BitcoinService(IHttpClientFactory clientFactory, ILogger<BitcoinService> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }

    public async Task<PriceResponse?> GetBitcoinPriceAsync()
    {
        var client = _clientFactory.CreateClient("CoinGecko");
        var response = await client.GetAsync("simple/price?ids=bitcoin&vs_currencies=usd");

        if (!response.IsSuccessStatusCode) return null;

        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true,
            IncludeFields = true 
        };

        var data = await response.Content.ReadFromJsonAsync<BitcoinPrice>(options);
        
        if (data?.Data == null)
        {
            _logger.LogWarning("JSON recebido desestruturado");
            return null;
        }

        return new PriceResponse(
            "BTC",
            data.Data.UsdPrice.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-US")),
            DateTime.UtcNow
        );
    }
}