using LifeManagementApp.Interfaces;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using LifeManagementApp.Views;


namespace LifeManagementApp
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
                });

#if DEBUG
    		builder.Logging.AddDebug();

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IJokeService, JokeService>();
            builder.Services.AddSingleton<NotesViewModel>();
            builder.Services.AddSingleton<AllNotesPage>();
#endif

            return builder.Build();
        }
    }
}
