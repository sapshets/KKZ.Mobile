using AccountingMobile.Views;

namespace AccountingMobile;

public partial class App : Application
{
    public App(AuthPage p)
    {
        InitializeComponent();

        MainPage = new AppShell();
        MainPage = new NavigationPage(p);
    }
}