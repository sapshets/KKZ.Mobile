using System.Collections.ObjectModel;
using AccountingMobile.Models;
using AccountingMobile.Popups;
using CommunityToolkit.Maui.Views;

namespace AccountingMobile.ViewModels;

public class CargoReceptionVm : BaseVm
{
    public CargoReceptionVm()
    {
        Cargos = new ObservableCollection<CargoModel>()
        {
            new CargoModel()
            {
                Name = "Вапно"
            },new CargoModel()
            {
                Name = "Ячмінь"
            },new CargoModel()
            {
                Name = "Кукурудза"
            },new CargoModel()
            {
                Name = "Кісткове борошно"
            },
        };
    }

    public ObservableCollection<CargoModel> Cargos { get; set; }
    
    private CargoModel _selectedCargo;

    public CargoModel SelectedCargo
    {
        get => _selectedCargo;
        set { _selectedCargo = value; }
    }

    public Command AddNewTypeCommand => new Command(async () =>
    {
        var r = await App.Current.MainPage.ShowPopupAsync(new AddItemPopup());
        if (r is CargoModel cargo)
        {
            Cargos.Add(cargo);
        }
    });

}