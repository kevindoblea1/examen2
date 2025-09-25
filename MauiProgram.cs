using CommunityToolkit.Mvvm; 
using Examen_Mvvm.ViewModels;

namespace Examen_Mvvm;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

  
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>(); 

        return builder.Build();
    }
}
