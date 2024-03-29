namespace MSPC_CRUD20231225.Blazor.Classes;

public class WebAPIInteraction
{
    private readonly HttpClient _httpClient;

    public WebAPIInteraction([FromKeyedServices("db")] HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<long> GetTableCount(string database, string table)
    {
        return await _httpClient.GetFromJsonAsync<long>($"api/AdvancedSearch_{database}_{table}/GetAllCount");

    }
}
