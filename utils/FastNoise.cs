using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Timberlands.Utils;
// TODO: see if that works. i did a lot of stuff without testing anything.
/* /// <summary>
///     A class for generating 2D perlin noise.
/// </summary>
public unsafe class FastNoise : IDisposable
{
    private const int GradientSizeTable = 256; // size of gradient lookup table
    private readonly int[] _permutationTable; // permutation table to avoid repeated calculations
    private readonly float* _gradients; // gradient values allocated in unmanaged memory

    /// <summary>
    ///     Initializes a new instance of <see cref="FastNoise"/> class.
    /// </summary>
    /// <param name="seed">Seed value for noise generation.</param>
    public FastNoise(int seed)
    {
        _permutationTable = new int[GradientSizeTable * 2];
        _gradients = (float*)NativeMemory.Alloc((nuint)(GradientSizeTable * 2 * sizeof(float)));

        Random rand = new(seed);
        for (int i = 0; i < GradientSizeTable; i++)
        {
            _permutationTable[i] = i;
            _gradients[i] = (float)(rand.NextDouble() * 2.0 - 1.0); // gradients in range [-1, 1]. balances output values.
        }

        // shuffle the permutation table (Fisher-Yates algorithm)
        for (int i = 0; i < GradientSizeTable; i++)
        {
            int swapIndex = rand.Next(GradientSizeTable);
            (_permutationTable[i], _permutationTable[swapIndex]) = (_permutationTable[swapIndex], _permutationTable[i]);
        }

        // duplicate permutation table for easier wrapping of indices
        for (int i = 0; i < GradientSizeTable; i++)
        {
            _permutationTable[GradientSizeTable + i] = _permutationTable[i];
        }
    }

    /// <summary>
    ///     Releases unmanaged memory used by the class.
    /// </summary>
    public void Dispose()
    {
        NativeMemory.Free(_gradients);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Calculates the gradient for a given hash and 2D coordinate.
    ///     "Why the bit tricks?" Because it’s faster than modular arithmetic.
    /// </summary>
    /// <param name="hash">Hashed value from the permutation table.</param>
    /// <param name="x">X-coordinate.</param>
    /// <param name="y">Y-coordinate.</param>
    /// <returns>Gradient value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float Grad(int hash, float x, float y)
    {
        int h = hash & 15; // only care about the last 4 bits.
        float u = h < 8 ? x : y; // choose which coordinates to use based on hash
        float v = h < 4 ? y : h == 12 || h == 14 ? x : 0; // decide second coordinates  
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v); // dot product of gradient and point
    }

    /// <summary>
    ///     Generates 2D Perlin noise for a given point.
    /// </summary>
    /// <param name="x">X-coordinate.</param>
    /// <param name="y">Y-coordinate.</param>
    /// <returns>Noise value in the range [-1, 1].</returns>
    public float Perlin(float x, float y)
    {
        // determine grid cell coordinates
        int X = FastMath.Floor(x) & (GradientSizeTable - 1);
        int Y = FastMath.Floor(y) & (GradientSizeTable - 1);

        // relative coordinates within the cell
        x -= FastMath.Floor(x);
        y -= FastMath.Floor(y);

        // compute fade curves for x and y
        float u = FastMath.Fade(x);
        float v = FastMath.Fade(y);

        // hash coordinates of the corners
        int A = _permutationTable[X] + Y;
        int B = _permutationTable[X + 1] + Y;

        // interpolate the noise value from the gradients at the corners
        return FastMath.Lerp(
            FastMath.Lerp(Grad(_permutationTable[A], x, y), Grad(_permutationTable[A + 1], x - 1, y), u),
            FastMath.Lerp(Grad(_permutationTable[B], x, y - 1), Grad(_permutationTable[B + 1], x - 1, y - 1), u),
            v
        );
    }
} */