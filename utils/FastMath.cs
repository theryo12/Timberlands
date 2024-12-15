using System;
using System.Runtime.CompilerServices;

namespace Timberlands.Utils;

/// <summary>
///     Utility class for common mathematical operations.
/// </summary>
public static class FastMath
{
    /// <summary>
    ///     Smooths the input value using a quintic fade curve.
    ///     This makes the transitions between gradients smooth.
    ///     Formula: 6t^5 - 15t^4 + 10t^3
    /// </summary>
    /// <param name="t">Input value (0 to 1).</param>
    /// <returns>Smoothed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);

    /// <summary>
    ///     Linearly interpolates between two values.
    ///     Formula: a + t * (b - a)
    /// </summary>
    /// <param name="a">Start value.</param>
    /// <param name="b">End value.</param>
    /// <param name="t">Interpolation factor.</param>
    /// <returns>Interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float a, float b, float t) => a + t * (b - a);

    /// <summary>
    ///     Faster alternative to MathF.Floor.
    /// </summary>
    /// <param name="x">Input value.</param>
    /// <returns>Floored integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Floor(float x) => (x >= 0) ? (int)x : (int)x - 1;

    /// <summary>
    ///     Clamps a value between a minimum and a maximum.
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <param name="min">Minimum value.</param>
    /// <param name="max">Maximum value.</param>
    /// <returns>Clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float value, float min, float max) => MathF.Max(min, MathF.Min(max, value));

    /// <summary>
    ///     Computes a smoothstep interpolation between two edges.
    ///     Formula: 3x^2 - 2x^3
    /// </summary>
    /// <param name="edge0">Lower edge.</param>
    /// <param name="edge1">Upper edge.</param>
    /// <param name="x">Interpolation factor.</param>
    /// <returns>Smoothed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SmoothStep(float edge0, float edge1, float x)
    {
        x = Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
        return x * x * (3 - 2 * x);
    }
}