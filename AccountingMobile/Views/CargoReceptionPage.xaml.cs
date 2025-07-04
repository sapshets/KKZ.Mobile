using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingMobile.ViewModels;

namespace AccountingMobile.Views;

public partial class CargoReceptionPage : ContentPage
{
    public CargoReceptionPage(CargoReceptionVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}