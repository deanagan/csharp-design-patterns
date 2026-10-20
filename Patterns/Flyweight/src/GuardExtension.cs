using System.Runtime.CompilerServices;

namespace Flyweight;

public static class GuardExtensions
{
    public static T ThrowIfNull<T>(this T? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
        return argument;
    }
}