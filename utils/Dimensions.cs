using System;

namespace Timberlands.Utils;

/// <summary>
///     Represents 2D dimensions with immutable width and height.
/// </summary>
/// <remarks>
///     Use this struct to encapsulate the width and height of an object, ensuring immutability and safe value 
///     semantics.
///     Dimensions must be always greater than zero.
/// </remarks>
public readonly struct Dimensions
{
    /// <summary>
    ///     The width of the object in unsigned 16-bit units.
    /// </summary>
    public readonly ushort Width;

    /// <summary>
    ///     The height of the object in unsigned 16-bit units.
    /// </summary>
    public readonly ushort Height;

    public Dimensions(ushort width, ushort height)
    {
        if (width == 0 || height == 0)
            throw new ArgumentException("Dimensions must be greater than zero!");
        Width = width;
        Height = height;
    }

    public override bool Equals(object? obj) => obj is Dimensions other && this == other;

    public override int GetHashCode() => HashCode.Combine(Width, Height);

    public override string ToString() => $"{Width} x {Height}";

    public static bool operator ==(Dimensions left, Dimensions right) => left.Width == right.Width && left.Height == right.Height;

    public static bool operator !=(Dimensions left, Dimensions right) => !(left == right);

    public static bool operator <(Dimensions left, Dimensions right) => left.Width < right.Width || (left.Width == right.Width && left.Height < right.Height);

    public static bool operator >(Dimensions left, Dimensions right) => left.Width > right.Width || (left.Width == right.Width && left.Height > right.Height);
}