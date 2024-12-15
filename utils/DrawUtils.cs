using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static Terraria.GameContent.TextureAssets;

namespace Timberlands.Utils;

public static class DrawUtils
{
    /// <summary>
    ///     Contains methods for drawing primitive shapes such as rectangles and lines.
    /// </summary>
    public static class Primitives
    {
        /// <summary>
        ///     Represents the style of a shape, including fill color, outline color and outline thickness.
        /// </summary>
        public readonly struct DrawStyle(Color fillColor, Color outlineColor, float outlineThickness = 1.0f)
        {
            public readonly Color FillColor = fillColor;
            public readonly Color OutlineColor = outlineColor;
            public readonly float OutlineThickness = outlineThickness;

            public static DrawStyle FillOnly(Color fillColor)
            {
                return new DrawStyle(fillColor, Color.Transparent, 0);
            }

            public static DrawStyle OutlineOnly(Color outlineColor, float thickness)
            {
                return new DrawStyle(Color.Transparent, outlineColor, thickness);
            }
        }

        /// <summary>
        /// Draws a rectangle with the specified style at the given position and size.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the rectangle.</param>
        /// <param name="position">The position of the top-left corner of the rectangle.</param>
        /// <param name="size">The dimensions (width and height) of the rectangle.</param>
        /// <param name="style">The DrawStyle that determines fill color, outline color, and outline thickness.</param>
        public static void Rect(SpriteBatch spriteBatch, Vector2 position, Dimensions size, DrawStyle style)
        {
            if (style.FillColor != Color.Transparent)
            {
                spriteBatch.Draw(MagicPixel.Value, new Rectangle((int)position.X, (int)position.Y, (int)size.Width, size.Height), style.FillColor);
            }

            // Outline
            if (style.OutlineColor != Color.Transparent && style.OutlineThickness > 0)
            {
                // Top
                spriteBatch.Draw(MagicPixel.Value, new Rectangle((int)position.X, (int)position.Y, size.Width, (int)style.OutlineThickness), style.OutlineColor);
                // Bottom
                spriteBatch.Draw(MagicPixel.Value, new Rectangle((int)position.X, (int)(position.Y + size.Height - style.OutlineThickness), size.Width, (int)style.OutlineThickness), style.OutlineColor);
                // Left
                spriteBatch.Draw(MagicPixel.Value, new Rectangle((int)position.X, (int)position.Y, (int)style.OutlineThickness, size.Height), style.OutlineColor);
                // Right
                spriteBatch.Draw(MagicPixel.Value, new Rectangle((int)(position.X + size.Width - style.OutlineThickness), (int)position.Y, (int)style.OutlineThickness, size.Height), style.OutlineColor);
            }
        }

        /// <summary>
        /// Draws a line from the start point to the end point with the specified color and thickness.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the line.</param>
        /// <param name="start">The starting point of the line.</param>
        /// <param name="end">The ending point of the line.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="thickness">The thickness of the line.</param>
        public static void Line(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            Vector2 direction = end - start;
            float length = direction.Length();
            float rotation = (float)Math.Atan2(direction.Y, direction.X);

            spriteBatch.Draw(
                MagicPixel.Value,
                start,
                null,
                color,
                rotation,
                Vector2.Zero,
                new Vector2(length, thickness),
                SpriteEffects.None,
                0f
            );
        }
    }
}
