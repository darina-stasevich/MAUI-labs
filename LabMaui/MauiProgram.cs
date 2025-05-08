using MauiApp6.Services;
using Microsoft.Extensions.Logging;

namespace LabMaui;

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
            });
        builder.Services.AddTransient<IDbService, SQLiteService>();
        builder.Services.AddHttpClient<IRateService, RateService>(
            opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"));
        builder.Services.AddTransient<SpaService>();
        builder.Services.AddTransient<Converter>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}