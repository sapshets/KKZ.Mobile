using AccountingMobile.Models;

namespace AccountingMobile.Services.ApiServices;

public class LoginService : BaseApiService<LoginRequest>
{
    public LoginService(HttpClient httpClient) : base(httpClient, "invoices")
    {
        
    }
}