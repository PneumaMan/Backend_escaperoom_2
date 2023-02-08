using Microsoft.Extensions.Options;
using Backend_Escaperoom_2.Domain.Settings;
using System;
using System.Globalization;
using System.Threading;

public class CultureScope : IDisposable
{
    private readonly CultureInfo _originalCulture;
    private readonly CultureInfo _originalUICulture;

    public CultureScope(CultureInfo culture)
    {
        this._originalCulture = Thread.CurrentThread.CurrentCulture;
        this._originalUICulture = Thread.CurrentThread.CurrentUICulture;

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = this._originalCulture;
        Thread.CurrentThread.CurrentUICulture = this._originalUICulture;
    }
}