using System.Collections.ObjectModel;
using AccountingMobile.Models;
using AccountingMobile.Popups;
using CommunityToolkit.Maui.Views;

namespace AccountingMobile.ViewModels;

public class CargoReceptionVm : BaseVm
{
    public CargoReceptionVm()
    {
        RawStaffs =
        [
            new RawStuff()
            {
                Name = "Вапно"
            },
            new RawStuff()
            {
                Name = "Ячмінь"
            },
            new RawStuff()
            {
                Name = "Кукурудза"
            },
            new RawStuff()
            {
                Name = "Кісткове борошно"
            }

        ];
    }

    public ObservableCollection<RawStuff> RawStaffs { get; set; }
    
    private RawStuff _selectedRawStuff;

    public RawStuff SelectedRawStuff
    {
        get => _selectedRawStuff;
        set { _selectedRawStuff = value; }
    }
    
    
    // public Command AddNewItemCommand => new Command(async () =>
    // {
    //     var r = await App.Current.MainPage.ShowPopupAsync(new AddItemPopup());
    //     if (r is CargoModel cargo)
    //     {
    //         Cargos.Add(cargo);
    //     }
    // });
    //
    // public Command SendCommand => new Command(() =>
    // {
    //     SendedObject = $"Об'єкт надіслано: {SelectedCargo.Name}-{SelectedCargo.Weight}кг";
    // });

}