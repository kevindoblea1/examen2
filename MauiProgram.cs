using CommunityToolkit.Mvvm; // Necesario por el paquete (genera code-behind)
using Examen_Mvvm.ViewModels;

namespace Examen_Mvvm;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        // DI (Inyección de dependencias)
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>(); // MainPage está en el namespace raíz

        return builder.Build();
    }
}
