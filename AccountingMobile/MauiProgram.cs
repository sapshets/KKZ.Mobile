using AccountingMobile.ViewModels;
using AccountingMobile.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace AccountingMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<CargoReceptionVm>();
        builder.Services.AddTransient<ReceiptVm>();
        builder.Services.AddTransient<InvoiceVm>();
        
        builder.Services.AddTransient<CargoReceptionPage>();
        builder.Services.AddTransient<ReceiptPage>();
        builder.Services.AddTransient<InvoicePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        
        #region Handlers

        EntryHandler.Mapper.AppendToMapping("NoPlatformStyle", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent); // Прибирає фон
            handler.PlatformView.Background = null; // Видаляє стандартний стиль (лінію під Entry)
            handler.PlatformView.SetPadding(0, 0, 0, 0); // Прибирає зайві відступи
            handler.PlatformView.TextCursorDrawable = null; // Прибирає стандартний курсор
#elif IOS
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });
        
        DatePickerHandler.Mapper.AppendToMapping("NoPlatformStyle", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetPadding(20, 20, 20, 20);
#elif IOS
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.Background = null;
#endif
        });
        
        PickerHandler.Mapper.AppendToMapping("NoPlatformStyle", (handler, view) =>
        {
            handler.PlatformView.Background = null;
        });
        #endregion Handlers
        
        return builder.Build();
    }
}