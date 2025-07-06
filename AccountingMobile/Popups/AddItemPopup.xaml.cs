using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingMobile.Models;

namespace AccountingMobile.Popups;

public partial class AddItemPopup
{
    public AddItemPopup()
    {
        InitializeComponent();
        BindingContext =  this;
    }
    
    private async void OK_Clicked(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameText.Text))
        {
            var item = new CargoModel()
            {
                // Name = NameText.Text
            };
            Close(item);
        }
        return;
    }
    private void Cancel_Clicked(object? sender, EventArgs e)
    {
        Close();
    }
}