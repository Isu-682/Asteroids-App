using Asteroids_App.Services;

namespace Asteroids_App
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

            // Registrar servicios
            builder.Services.AddSingleton<NeoService>();

            return builder.Build();
        }
    }
}
