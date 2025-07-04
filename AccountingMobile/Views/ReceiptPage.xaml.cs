using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingMobile.ViewModels;

namespace AccountingMobile.Views;

public partial class ReceiptPage : ContentPage
{
    public ReceiptPage(ReceiptVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}