using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Langy.Core;

public static class VerifyThat
{
    public static void IsNotNull<T>([NotNull] T? value, 
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value == null)
            throw new ArgumentNullException(paramName);
    }
}