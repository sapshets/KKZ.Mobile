namespace AccountingMobile.Models;

public class InvoiceModel : BaseModel
{
    public int InvoiceNumber { get; set; }
    public DateTime CreateDate { get; set; }
    public int EmployeeId { get; set; }
    public List<CargoModel> Cargos { get; set; }
}