using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls;

namespace Examen_Mvvm.Comportamientos;

/// <summary>
/// Permite solo dígitos y UN separador decimal (según la cultura actual).
/// Bloquea letras, espacios y múltiples separadores. Soporta pegar texto.
/// </summary>
public class ComportamientoEntradaNumerica : Behavior<Entry>
{
    private readonly string _decimalSeparator =
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

    protected override void OnAttachedTo(Entry entry)
    {
        base.OnAttachedTo(entry);
        entry.TextChanged += OnTextChanged;
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        var text = e.NewTextValue ?? string.Empty;

        // Permite vacío (borrar todo)
        if (text.Length == 0)
            return;

        bool hasSeparator = false;
        var sb = new StringBuilder();

        foreach (var ch in text)
        {
            if (char.IsDigit(ch))
            {
                sb.Append(ch);
            }
            else if (ch.ToString() == _decimalSeparator && !hasSeparator)
            {
                if (sb.Length == 0) sb.Append('0'); // ".5" -> "0.5"
                sb.Append(_decimalSeparator);
                hasSeparator = true;
            }
            // otros caracteres se ignoran
        }

        var sanitized = sb.ToString();

        // Evitar bucles de asignación
        if (!string.Equals(text, sanitized, StringComparison.Ordinal))
            entry.Text = sanitized;
    }
}
