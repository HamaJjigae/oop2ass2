using Traveless.ViewModels;
using Traveless.Services;
using Microsoft.Extensions.Logging;
using Traveless.Models;

namespace Traveless
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<AirportCSVHandler>();
            builder.Services.AddSingleton<FlightCSVHandler>();
            builder.Services.AddSingleton(provider =>
            {
                var airportDict = provider.GetRequiredService<AirportCSVHandler>().ReadAiportCSVToDict();
                var flightList = provider.GetRequiredService<FlightCSVHandler>().flightList();
                return new FindFlights(flightList, airportDict);
            });
            builder.Services.AddSingleton(new List<MakeReservation>());
            builder.Services.AddSingleton<ReservationManager>();

            builder.Services.AddTransient<FlightsViewModel>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
