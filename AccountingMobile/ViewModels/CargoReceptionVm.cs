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
                RawStaffs.Add(new RawStuff { Name = stuff.Name, Id = stuff.Id}); 
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
            await Shell.Current.DisplayAlert("Помилка", "Неможливо надіслати порожню накладну.", "OK");
            return;
        }

        // Перевіряємо, чи для кожного вантажу обрано сировину
        if (Cargos.Any(c => c.SelectedRawStuff == null || c.SelectedRawStuff.Id == 0))
        {
            await Shell.Current.DisplayAlert("Помилка", "Для кожного вантажу необхідно обрати сировину.", "OK");
            return;
        }
    
        // --- Формуємо правильний об'єкт для відправки на API ---
        var invoiceToSend = new 
        {
            Invoice.InvoiceNumber,
            Invoice.EmployeeId,
            // Створюємо список вантажів, який відповідає DTO на бекенді
            Cargos = Cargos.Select(c => new 
            {
                Weight = c.Weight, // Припускаючи, що у CargoModel є властивість Weight
                RawStuffId = c.SelectedRawStuff.Id // <<< Головна зміна: використовуємо ID обраної сировини
            }).ToList()
        };

        try
        {
            // Відправляємо новостворений об'єкт
            var response = await _invoiceService.SendInvoiceAsync(invoiceToSend); // Потрібно оновити SendInvoiceAsync
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Успіх!", "Накладну успішно надіслано.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка", "Не вдалося надіслати накладну.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Критична помилка", $"Виникла помилка: {ex.Message}", "OK");
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