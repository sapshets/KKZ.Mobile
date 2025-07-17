using System.Collections.ObjectModel;
using AccountingMobile.Models;
using AccountingMobile.Popups;
using CommunityToolkit.Maui.Views;

namespace AccountingMobile.ViewModels;

public class CargoReceptionVm : BaseVm, IQueryAttributable
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
        AddNewItemCommand.Execute(null);
    }

    public InvoiceModel Invoice { get; set; }
    public ObservableCollection<RawStuff> RawStaffs { get; set; }
    
    private RawStuff _selectedRawStuff;

    public RawStuff SelectedRawStuff
    {
        get => _selectedRawStuff;
        set { _selectedRawStuff = value; }
    }

    public ObservableCollection<CargoModel> Cargos { get; set; } = new();
    
    public Command AddNewItemCommand => new Command(async () =>
    {
        Cargos.Add(new()
        {
            SelectedRawStuff = new RawStuff()
            {
                Name = ""
            }
        });
    });
    
    public Command SendCommand => new Command(() =>
    {
        var r = Cargos;
        if (r.Any() && Invoice != null)
        {
            Invoice.Cargos.AddRange(r);
        }
        
    });
    public Command<CargoModel> DeleteCommand => new Command<CargoModel>((cargo) =>
    {
        Cargos.Remove(cargo);
    });


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Invoice = query["invoice"] as InvoiceModel;
    }
}