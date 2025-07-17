using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AccountingMobile.Services.ApiServices;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService()
    {
        _httpClient = new HttpClient();
    }

    // Метод для входу в систему
    public async Task<bool> LoginAsync(string username, string password)
    {
        var request = new { Username = username, Password = password };
        
        // Відправляємо запит
        var response = await _httpClient.PostAsJsonAsync($"{Constants.API_KEY}Auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            
            // Зберігаємо токен у безпечне сховище пристрою
            await SecureStorage.Default.SetAsync("jwt_token", token);
            
            // Встановлюємо токен у заголовок для всіх майбутніх запитів
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            return true;
        }

        return false;
    }

    // Метод для виходу
    public void Logout()
    {
        // Видаляємо токен зі сховища
        SecureStorage.Default.Remove("jwt_token");
        
        // Прибираємо заголовок авторизації
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    // Метод для перевірки токена при запуску додатку
    public async Task InitializeAsync()
    {
        var token = await SecureStorage.Default.GetAsync("jwt_token");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        await Shell.Current.GoToAsync("//tabs");
    }
}