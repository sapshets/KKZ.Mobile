using AccountingMobile.Models;
using AccountingMobile.Views;

namespace AccountingMobile.ViewModels;

public class InvoiceVm
{
    public InvoiceModel Invoice { get; set; } = new InvoiceModel();

    public Command ContinueCommand => new Command( async () =>
    {
        try
        {
            if (string.IsNullOrEmpty(Invoice.InvoiceNumber))
            {
                return;
            }
            var l = Shell.Current.CurrentState.Location.ToString();
            Invoice.CreateDate = DateTime.Now;
            Invoice.EmployeeId = StaticData.UserId;
        
            var navParam = new ShellNavigationQueryParameters
            {
                {"invoice", Invoice},
            };
            await Shell.Current.GoToAsync($"//tabs/{nameof(CargoReceptionPage)}",  navParam);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    });
}