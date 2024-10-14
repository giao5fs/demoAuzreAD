using System.Text.Json.Serialization;
using Azure.Core;
using Azure.Identity;
using Newtonsoft.Json;

namespace api1.Utilities;

public class ApiUtility
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiUtility> _logger;
    public ApiUtility(IHttpClientFactory httpFactory, IConfiguration configuration, ILogger<ApiUtility> logger)
    {
        _logger = logger;
        _httpClient = httpFactory.CreateClient();
        _configuration = configuration;

        var battlehungerApi2Url = _configuration.GetValue<string>("ApiConfig:movelorenciaApi2");

        _httpClient.BaseAddress = new Uri(battlehungerApi2Url!);
    }

    public async Task<T?> GetData<T>()
    {
        var api2ClientId = _configuration.GetValue<string>("AzureAD:Api2ClientId");
        var credential = new DefaultAzureCredential();
        var tokenContext = new TokenRequestContext(scopes: [$"{api2ClientId}/.default"]);
        string token = string.Empty;
        try
        {
            var accessToken = await credential.GetTokenAsync(tokenContext);
            token = accessToken.Token;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        _logger.LogInformation(token);

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("weatherforecast");
        response.EnsureSuccessStatusCode();

        var res = await response.Content.ReadFromJsonAsync<T>();

        _logger.LogInformation("Result____________:");

        _logger.LogInformation(JsonConvert.SerializeObject(res));

        return res;
    }
}
