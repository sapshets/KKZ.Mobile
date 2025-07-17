using System.Net.Http.Json;

namespace AccountingMobile.Services.ApiServices;

public class BaseApiService<T> where T : class
{
    protected readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public BaseApiService(HttpClient httpClient, string endpoint)
    {
        _httpClient = httpClient;
        _endpoint = Constants.API_KEY+endpoint;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<T>>(_endpoint);
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<T>($"{_endpoint}/{id}");
    }

    public virtual async Task<HttpResponseMessage> CreateAsync(T entity)
    {
        return await _httpClient.PostAsJsonAsync(_endpoint, entity);
    }

    public virtual async Task<HttpResponseMessage> UpdateAsync(int id, T entity)
    {
        return await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", entity);
    }

    public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _httpClient.DeleteAsync($"{_endpoint}/{id}");
    }
}