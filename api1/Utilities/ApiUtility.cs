using System.Net;
using System.Text.Json;
using Azure.Core;
using Azure.Identity;

namespace api1.Utilities;

public class ApiUtility
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiUtility> _logger;
    public ApiUtility(IHttpClientFactory httpFactory, IConfiguration configuration, ILogger<ApiUtility> logger)
    {
        _httpClient = httpFactory.CreateClient();
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri(_configuration["ApiConfig:BattlehungerApi2"] ?? "");
        _logger = logger;
    }

    public async Task<T?> GetData<T>()
    {
        _logger.LogInformation("Start GetData..........");
        var api2ClientId = "1f0f922f-ed3e-486b-961c-cb0ad598fc0e";
        _logger.LogError(api2ClientId);
        var credential = new DefaultAzureCredential();

        var tokenContext = new TokenRequestContext(scopes: [$"{api2ClientId}/.default"]);

        var accessToken = await credential.GetTokenAsync(tokenContext);

        _logger.LogError(accessToken.Token);
        _logger.LogInformation("Start calling API2.....");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Token);

        var response = await _httpClient.GetAsync("weatherforecast");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
