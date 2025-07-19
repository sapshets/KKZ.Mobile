using AccountingMobile.Services.ApiServices;
using CommunityToolkit.Mvvm.Input;

namespace AccountingMobile.ViewModels;

public partial class AuthVm(AuthService authService) : BaseVm
{
    public string Username { get; set; }
    public string Password { get; set; }
    [RelayCommand]
    private async Task LoginAsync()
    {
        var res = await authService.LoginAsync(Username, Password);
        if (res != null)
        {
            // Перехід на головну сторінку
            await Shell.Current.GoToAsync("//tabs");
        }
        else
        {
            // Показати помилку
            await Shell.Current.DisplayAlert("Помилка", "Неправильний логін або пароль", "OK");
        }
    }
}