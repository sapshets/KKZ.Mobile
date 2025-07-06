using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingMobile.ViewModels;

namespace AccountingMobile.Views;

public partial class InvoicePage : ContentPage
{
    public InvoicePage(InvoiceVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}