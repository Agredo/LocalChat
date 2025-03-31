using CommunityToolkit.Maui;
using LocalChat.Maui.Views.Pages;
using Microsoft.Extensions.Logging;
using LocalChat.ViewModels.Pages;

namespace LocalChat
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            addServices(builder.Services);

            return builder.Build();
        }

        private static void addServices(IServiceCollection services)
        {
            services.AddTransient<StartPage, StartPageViewModel>();
        }
    }
}
