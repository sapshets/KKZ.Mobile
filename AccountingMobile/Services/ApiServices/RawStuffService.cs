using AccountingMobile.Models;

namespace AccountingMobile.Services.ApiServices;

public class RawStuffService : BaseApiService<RawStuffDto>
{
    public RawStuffService(HttpClient httpClient) : base(httpClient, "rawstuffs")
    {
    }
}