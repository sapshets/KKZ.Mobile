using AccountingMobile.Models;
using AccountingMobile.Views;

namespace AccountingMobile.ViewModels;

public class InvoiceVm
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();

    public Command ContinueCommand => new Command( async () =>
    {
        if (Invoice.InvoiceNumber == 0)
        {
            return;
        }
        var l = Shell.Current.CurrentState.Location.ToString();
        Invoice.CreateDate = DateTime.Now;
        Invoice.EmployeeId = 1;//TODO there is should be employee id after authorisation
        
        var navParam = new ShellNavigationQueryParameters
        {
            {"invoice", Invoice},
        };
       await Shell.Current.GoToAsync($"{l}/{nameof(CargoReceptionPage)}",  navParam);
    });
}