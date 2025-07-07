using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingMobile.ViewModels;

namespace AccountingMobile.Views;

public partial class AuthPage : ContentPage
{
    public AuthPage(AuthVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}