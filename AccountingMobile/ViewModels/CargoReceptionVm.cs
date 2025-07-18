using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AccountingMobile.Models;
using AccountingMobile.Services;
using AccountingMobile.Services.ApiServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccountingMobile.ViewModels;

public partial class CargoReceptionVm : BaseVm, IQueryAttributable
{
    private readonly RawStuffService _rawStuffService;
    private readonly InvoiceService _invoiceService;
    [ObservableProperty]
    private InvoiceModel _invoice;

    [ObservableProperty]
    private ObservableCollection<RawStuff> _rawStaffs;

    [ObservableProperty]
    private ObservableCollection<CargoModel> _cargos;
    
    public CargoReceptionVm(RawStuffService rawStuffService, InvoiceService invoiceService)
    {
        _rawStuffService = rawStuffService;
        _invoiceService = invoiceService;
        Cargos = new ObservableCollection<CargoModel>();
        RawStaffs = new ObservableCollection<RawStuff>();

        AddNewItemCommand.Execute(null);
    }
    
    [RelayCommand]
    private async Task LoadRawStuffsAsync()
    {
        try
        {
            var stuffs = await _rawStuffService.GetAllAsync();
            RawStaffs.Clear();
            foreach (var stuff in stuffs)
            {
                RawStaffs.Add(new RawStuff { Name = stuff.Name }); 
            }
        }
        catch (Exception ex)
        {
            // Обробка помилки
            Console.WriteLine($"Не вдалося завантажити сировину: {ex.Message}");
        }
    }

    [RelayCommand]
    private void AddNewItem()
    {
        Cargos.Add(new CargoModel
        {
            SelectedRawStuff = new RawStuff() { Name = "" }
        });
    }

    
    [RelayCommand]
    private async Task SendAsync()
    {
        if (!Cargos.Any() || Invoice == null)
        {
            // Показати помилку, якщо немає вантажів
            return;
        }

        // Додаємо зібрані вантажі до моделі накладної
        Invoice.Cargos.Clear();
        foreach (var cargo in Cargos)
        {
            Invoice.Cargos.Add(cargo);
        }

        try
        {
            // Викликаємо метод сервісу для відправки
            var response = await _invoiceService.SendInvoiceAsync(Invoice);
            if (response.IsSuccessStatusCode)
            {
                // Успіх! Повертаємось на попередню сторінку
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                // Обробка помилки від сервера
                await Shell.Current.DisplayAlert("Помилка", "Не вдалося надіслати накладну.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Обробка помилки запиту
            await Shell.Current.DisplayAlert("Помилка", $"Виникла помилка: {ex.Message}", "OK");
        }
    }
    
    [RelayCommand]
    private void Delete(CargoModel cargo)
    {
        if (cargo != null)
        {
            Cargos.Remove(cargo);
        }
    }
    
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Invoice = query["invoice"] as InvoiceModel;
        await LoadRawStuffsAsync(); 
    }
}