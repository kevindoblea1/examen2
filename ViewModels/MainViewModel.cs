using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace Examen_Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject
{
    // Entradas (como texto para validar y evitar excepciones)
    [ObservableProperty] private string product1;
    [ObservableProperty] private string product2;
    [ObservableProperty] private string product3;

    // Campos calculados
    [ObservableProperty] private decimal subtotal;
    [ObservableProperty] private int descuento;    // porcentaje (0,10,20,30)
    [ObservableProperty] private decimal total;

    // Botón: Calcular
    [RelayCommand]
    private async Task CalcularAsync()
    {
        // Validación de vacíos
        if (string.IsNullOrWhiteSpace(Product1) ||
            string.IsNullOrWhiteSpace(Product2) ||
            string.IsNullOrWhiteSpace(Product3))
        {
            await Application.Current!.MainPage!.DisplayAlert("Datos incompletos",
                "Ingrese los tres valores de producto.", "OK");
            return;
        }

        // Parseo seguro respetando la cultura actual (coma o punto)
        var style = NumberStyles.Number;
        var culture = CultureInfo.CurrentCulture;

        if (!decimal.TryParse(Product1, style, culture, out var p1) ||
            !decimal.TryParse(Product2, style, culture, out var p2) ||
            !decimal.TryParse(Product3, style, culture, out var p3))
        {
            await Application.Current!.MainPage!.DisplayAlert("Formato inválido",
                "Use solo números (ej. 1234.56).", "OK");
            return;
        }

        // No permitir negativos
        if (p1 < 0 || p2 < 0 || p3 < 0)
        {
            await Application.Current!.MainPage!.DisplayAlert("Valores inválidos",
                "Los valores no pueden ser negativos.", "OK");
            return;
        }

        Subtotal = p1 + p2 + p3;

        // Regla de descuentos (según rangos)
        Descuento = GetDiscountPercentage(Subtotal);

        // Total a pagar
        var descuentoMonto = Subtotal * Descuento / 100m;
        Total = Subtotal - descuentoMonto;
    }

    // Botón: Limpiar
    [RelayCommand]
    private void Limpiar()
    {
        Product1 = string.Empty;
        Product2 = string.Empty;
        Product3 = string.Empty;
        Subtotal = 0m;
        Descuento = 0;
        Total = 0m;
    }

    private static int GetDiscountPercentage(decimal subtotal)
    {
        if (subtotal >= 1000m && subtotal <= 4999.99m) return 10;
        if (subtotal >= 5000m && subtotal <= 9999.99m) return 20;
        if (subtotal >= 10000m && subtotal <= 19999.99m) return 30;
        if (subtotal >= 20000m) return 0;
        // L 0.00 a L 999.99 => 0
        return 0;
    }
}
