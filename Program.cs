using BitcoinPriceCheckerDotNet.Services;
using BitcoinPriceCheckerDotNet.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("CoinGecko", client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
    // A API da CoinGecko é bem chatinha e bloqueia requisições sem um User-Agent realista
    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<BitcoinService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/get-bitcoin-price", async (IHttpClientFactory clientFactory) =>
{
    try {
        var client = clientFactory.CreateClient("CoinGecko");
        
        var response = await client.GetAsync("simple/price?ids=bitcoin&vs_currencies=usd");
        
        if (!response.IsSuccessStatusCode) 
        {
            var content = await response.Content.ReadAsStringAsync();
            return Results.Problem($"Status: {response.StatusCode} - Conteúdo: {content}");
        }

        var data = await response.Content.ReadFromJsonAsync<BitcoinPrice>();

        return Results.Ok(new PriceResponse(
            "BTC", 
            data?.Data.UsdPrice.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-US")) ?? "0", 
            DateTime.UtcNow
        ));
    } 
    catch (HttpRequestException ex)
    {
        return Results.Problem($"Erro de conexão: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERRO REAL: {ex.Message}");
        return Results.Problem("Ocorreu um erro interno inesperado.");
    }
});

app.Run();
