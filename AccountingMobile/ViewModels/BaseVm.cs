using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccountingMobile.ViewModels;

public class BaseVm : ObservableObject, INotifyPropertyChanged
{
    internal BaseVm()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;
public Command BackCommand  => new Command(async () => await Shell.Current.GoToAsync(".."));
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}