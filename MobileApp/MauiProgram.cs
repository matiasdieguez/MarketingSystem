using Microsoft.Extensions.Logging;
using MarketingSystem.MobileApp.Data;

namespace MarketingSystem.MobileApp;

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
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();


#if DEBUG
        builder.Services.AddScoped(sp => new HttpClient(GetDebugHttpHandler()));
#else
        builder.Services.AddScoped(sp => new HttpClient());
#endif
        return builder.Build();
    }

    public static HttpClientHandler GetDebugHttpHandler()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        }
        };
        return handler;
    }
}
