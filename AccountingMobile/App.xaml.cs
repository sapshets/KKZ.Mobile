using AccountingMobile.Services.ApiServices;
using AccountingMobile.Views;

namespace AccountingMobile;

public partial class App : Application
{
    public App(AuthService  authService)
    {
        InitializeComponent();

        MainPage = new AppShell();
        Task.Run( async () => await authService.InitializeAsync());
        
    }
}