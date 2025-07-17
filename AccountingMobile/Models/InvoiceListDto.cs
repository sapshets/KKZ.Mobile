namespace AccountingMobile.Models;

public record InvoiceListDto
{
    public int Id { get; init; }
    public string InvoiceNumber { get; init; }
    public DateTime CreateDate { get; init; }
    public string? EmployeeUsername { get; init; }
}