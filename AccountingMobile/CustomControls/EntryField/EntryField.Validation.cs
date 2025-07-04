namespace AccountingMobile.CustomControls.EntryField;

public partial class EntryField
{
    private static void ValidateNumericInput(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }
            if (!double.TryParse(e.NewTextValue, out _))
            {
                entry.Text = e.OldTextValue;
            }
        }
    }

    private static void ValidateDateTimeInput(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && !DateTime.TryParse(e.NewTextValue, out _))
        {
            entry.Text = e.OldTextValue;
        }
    }
}