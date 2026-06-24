using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BYOK.Core.Utils;

public static class Throw
{
    #region Object
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [return: NotNull]
    public static T IfNull<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : class
        => value ?? throw new ArgumentNullException(paramName);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [return: NotNull]
    public static T IfNull<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : struct
        => value ?? throw new ArgumentNullException(paramName);
    #endregion

    #region Strings
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [return: NotNull]
    public static string IfNullOrEmpty(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is null) throw new ArgumentNullException(paramName);
        if (value.Length == 0) throw new ArgumentException("Value cannot be empty", paramName);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [return: NotNull]
    public static string IfNullOrWhiteSpace(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is null) throw new ArgumentNullException(paramName);
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be whitespace", paramName);
        return value;
    }
    #endregion
}