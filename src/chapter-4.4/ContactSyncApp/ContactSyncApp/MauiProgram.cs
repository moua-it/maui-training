using ContactSyncApp.Dal;
using ContactSyncApp.View;
using ContactSyncApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace ContactSyncApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(DatePicker), typeof(Microsoft.Maui.Handlers.DatePickerHandler));
                    handlers.AddHandler(typeof(TimePicker), typeof(Microsoft.Maui.Handlers.TimePickerHandler));

                    Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                    {
#if IOS
                    if (handler.PlatformView is UIKit.UITextField nativeDatePicker)
                    {
                        nativeDatePicker.BorderStyle = UIKit.UITextBorderStyle.None;
                    }
#endif

#if ANDROID
                        if (handler.PlatformView is Android.Widget.EditText nativeDatePicker)
                        {
                            nativeDatePicker.Background = null;
                        }
#endif
                    });
                });

            builder.Services.AddTransient<ContactViewModel>();
            builder.Services.AddTransient<ContactFormPage>();
            builder.Services.AddSingleton(typeof(ContactSyncDatabase<>));
            builder.Services.AddScoped<ContactRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
