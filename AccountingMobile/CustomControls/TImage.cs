using CommunityToolkit.Maui.Behaviors;

namespace AccountingMobile.CustomControls;

public class TImage : Image
{
    public static readonly BindableProperty TintColorProperty =
        BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(TImage), propertyChanged: OnTintColorChanged);

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TImage)bindable;
        control.UpdateTintColor((Color)newValue);
    }

    private void UpdateTintColor(Color color)
    {
        var behavior = this.Behaviors.OfType<IconTintColorBehavior>().FirstOrDefault();
        if (behavior == null)
        {
            behavior = new IconTintColorBehavior();
            this.Behaviors.Add(behavior);
        }
        behavior.TintColor = color;
    }
}