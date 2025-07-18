using System.Net.Http.Json;
using AccountingMobile.Models;

namespace AccountingMobile.Services.ApiServices;

public class InvoiceService : BaseApiService<InvoiceListDto>
{
    public InvoiceService(HttpClient httpClient) : base(httpClient, "invoices") { }

    // <<< Додайте цей новий метод
    public async Task<HttpResponseMessage> SendInvoiceAsync(InvoiceModel invoice)
    {
        return await _httpClient.PostAsJsonAsync("invoices", invoice);
    }
}