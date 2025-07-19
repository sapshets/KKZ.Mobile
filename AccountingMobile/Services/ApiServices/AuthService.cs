using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AccountingMobile.Models;

namespace AccountingMobile.Services.ApiServices;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Метод для входу в систему
    public async Task<UserDto?> LoginAsync(string username, string password)
    {
        var request = new { Username = username, Password = password };

        var response = await _httpClient.PostAsJsonAsync("Auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            // Десеріалізуємо відповідь у наш новий DTO
            var loginResponse = await response.Content.ReadFromJsonAsync<UserDto>();
            StaticData.UserId = loginResponse.UserId;
            StaticData.Username = loginResponse.UserName;

            if (loginResponse != null)
            {
                // Зберігаємо токен у безпечне сховище
                await SecureStorage.Default.SetAsync("jwt_token", loginResponse.Token);

                // Встановлюємо токен у заголовок для всіх майбутніх запитів
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                return loginResponse;
            }
        }

        return null; // Повертаємо null у разі невдачі
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

    public async Task<bool> InitializeAsync()
    {
        var token = await SecureStorage.Default.GetAsync("jwt_token");

        if (IsTokenValid(token))
        {
            // Якщо токен валідний, встановлюємо заголовок
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await Shell.Current.GoToAsync("//tabs");
            return true;
        }

        // Якщо токен невалідний, чистимо сховище
        Logout();
        return false;
    }

    private bool IsTokenValid(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            // Нам не потрібно валідувати підпис на клієнті,
            // тому ми просто читаємо токен, щоб дістати дату
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Перевіряємо, чи не закінчився термін дії
            return jwtToken.ValidTo > DateTime.UtcNow;
        }
        catch
        {
            // Якщо токен не вдалося прочитати, він невалідний
            return false;
        }
    }
}