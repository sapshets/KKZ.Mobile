namespace AccountingMobile.Models;

public class CargoModel
{
    private RawStuff _selectedRawStuff;
    private int _weight;

    public RawStuff SelectedRawStuff { get; set; }

    public int Weight { get; set; }
}