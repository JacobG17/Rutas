using Microsoft.Extensions.Logging;
using Maui.GoogleMaps.Hosting;

namespace googlemaps;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			//Agrego la API de GoogleMaps para usarlo en IOS
			.UseGoogleMaps("AIzaSyDh-u7X4k-FwzkipuogQdo0s-JfEs6kvhI")
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

