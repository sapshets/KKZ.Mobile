namespace AccountingMobile.ViewModels;

public class AuthVm : BaseVm
{
    public Command LoginCommand => new Command( async () =>
    {
       await Shell.Current.GoToAsync("//tabs");
    });
}