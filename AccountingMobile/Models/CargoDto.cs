namespace AccountingMobile.Models;

public record CargoDto
{
    public int Id { get; init; }
    public int Weight { get; init; }
    public int? Remnant { get; init; }
    public int RawStuffId { get; init; }
    public int InvoiceId { get; init; }
}