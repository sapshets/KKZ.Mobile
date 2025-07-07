using AccountingMobile.Views;

namespace AccountingMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CargoReceptionPage),  typeof(CargoReceptionPage));
        Routing.RegisterRoute(nameof(AuthPage),  typeof(AuthPage));
    }
}