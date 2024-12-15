using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Timberlands.Utils;

namespace Timberlands.Common.World;

/// <summary>
///     Represents a fundamental block of the world, either static or dynamic.
///     WorldBlocks define fixed or variable sections of the Timberlands world during
///     world generation.
/// </summary>
public readonly struct WorldBlock
{
    /// <summary>
    ///     The type of the WorldBlock, determining its generation behavior.
    /// </summary>
    public enum BlockType : byte
    {
        /// <summary>
        ///     A static block, generated at a fixed position with a fixed size.
        /// </summary>
        Static,

        /// <summary>
        ///     A dynamic block, generated at a random position within the defined 
        ///     constraints.
        /// </summary>
        Dynamic
    }

    /// <summary>
    ///     Unique identifier for the block, calculated as a hash.
    /// </summary>
    public readonly ulong ID;

    /// <summary>
    ///     The type of WorldBlock. See <see cref="BlockType"/>.
    /// </summary>
    public readonly BlockType Type;

    /// <summary>
    ///     The minimum size of the block.
    /// </summary>
    /// <remarks>
    ///     For static blocks, <see cref="MinSize"/> and <see cref="MaxSize"/> are identical, as their size
    ///     is fixed.
    ///     For dynamic blocks, <see cref="MinSize"/> represents the smallest possible size the block can have 
    ///     during world generation.
    /// </remarks>
    public readonly Dimensions MinSize;

    /// <summary>
    ///     The maximum size of the block.
    /// </summary>
    /// <remarks>
    ///     For static blocks, <see cref="MinSize"/> and <see cref="MaxSize"/> are identical, as their size
    ///     is fixed.
    ///     For dynamic blocks, <see cref="MaxSize"/> represents the largest possible size the block can have
    ///     during world generation.
    /// </remarks>
    public readonly Dimensions MaxSize;

    /// <summary>
    ///     The position of the block in world coordinates.
    ///     For dynamic blocks, this is determined during generation.
    /// </summary>
    public readonly Vector2 Position;

    /// <summary>
    ///     Constructs a new <see cref="WorldBlock"/> with specified parameters.
    /// </summary>
    /// <param name="id">Unique identifier for the block, must be non-zero.</param>
    /// <param name="type">The type of the block (see <see cref="BlockType"/>).</param>
    /// <param name="minDimensions">The minimum size of the block.</param>
    /// <param name="maxDimensions">The maximum size of the block.</param>
    /// <param name="position">The fixed position for static blocks, or initial position for dynamic blocks.</param>
    /// <exception cref="ArgumentException">Thrown if dimensions or ID are invalid.</exception>
    public WorldBlock(ulong id, BlockType type, Dimensions minSize, Dimensions maxSize, Vector2 position)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be non-zero.", nameof(id));

        if (minSize > maxSize)
            throw new ArgumentException("Minimum dimensions cannot exceed maximum dimensions.", nameof(minSize));

        ID = id;
        Type = type;
        MinSize = minSize;
        MaxSize = maxSize;
        Position = position;
    }

    /// <summary>
    ///     Calculates the actual size of the block based on its <see cref="Type"/>.
    ///     For static blocks, the size is fixed. For dynamic blocks, a random size
    ///     within the constraints is generated.
    /// </summary>
    /// <param name="random">
    ///     An instance of <see cref="Random"/> for generating 
    ///     dynamic sizes.
    /// </param>
    /// <returns>The actual width of the block.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dimensions CalculateSize(ref Random random)
    {
        var width = Type == BlockType.Static ? MinSize.Width : random.Next(MinSize.Width, MaxSize.Width + 1);
        var height = Type == BlockType.Static ? MinSize.Height : random.Next(MinSize.Height, MaxSize.Height + 1);
        return new Dimensions((ushort)width, (ushort)height);
    }

    /// <summary>
    ///     Draw method for debugging purposes. Draws the WorldBlock using <see cref="DrawUtils"/>.
    /// </summary>
    /// <param name="random">An instance of <see cref="Random"/> for generating sizes for dynamic blocks.</param>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/> used for drawing.</param>
    /// <param name="style"><see cref="DrawUtils.Primitives.DrawStyle"/> determining how the block will look.</param>
    /// <param name="position">Position to draw block at.</param>
    public void Draw(ref Random random, SpriteBatch spriteBatch, DrawUtils.Primitives.DrawStyle style, Vector2 position)
    {
        DrawUtils.Primitives.Rect(spriteBatch, position, CalculateSize(ref random), style);
    }

    /// <summary>
    ///     Generates a unique hash-based ID for the block.
    /// </summary>
    /// <param name="name">The name or descriptor of the block.</param>
    /// <param name="seed">A seed value to ensure determinism across generations.</param>
    /// <returns>A unique hash-based identifier.</returns>
    public static ulong GenerateID(string name, int seed)
    {
        unchecked
        {
            ulong hash = (ulong)seed;
            foreach (char c in name)
            {
                hash = (hash * 31) + c;
            }
            return hash;
        }
    }
}