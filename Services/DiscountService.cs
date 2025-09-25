using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Mvvm.Services;

public static class DiscountService
{
    public static int GetDiscountPercentage(decimal subtotal)
    {
        if (subtotal >= 1000m && subtotal <= 4999.99m) return 10;
        if (subtotal >= 5000m && subtotal <= 9999.99m) return 20;
        if (subtotal >= 10000m && subtotal <= 19999.99m) return 30;
        if (subtotal >= 20000m) return 0;
        return 0;
    }

    public static (decimal Subtotal, int Descuento, decimal Total) Calculate(decimal p1, decimal p2, decimal p3)
    {
        if (p1 < 0 || p2 < 0 || p3 < 0)
            throw new ArgumentOutOfRangeException(nameof(p1), "Los valores no pueden ser negativos.");

        var subtotal = p1 + p2 + p3;
        var pct = GetDiscountPercentage(subtotal);
        var total = subtotal - (subtotal * pct / 100m);
        return (subtotal, pct, total);
    }
}
