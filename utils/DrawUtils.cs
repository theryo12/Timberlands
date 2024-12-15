using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Timberlands.Utils;

public static class DrawUtils
{
    private static Texture2D _pixelTexture;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _pixelTexture = new Texture2D(graphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { Color.White });
    }

    public static void Dispose()
    {
        _pixelTexture?.Dispose();
        _pixelTexture = null;
    }

    public static class Primitives
    {
        public readonly struct DrawStyle
        {
            public readonly Color FillColor;
            public readonly Color OutlineColor;
            public readonly float OutlineThickness;

            public DrawStyle(Color fillColor, Color outlineColor, float outlineThickness = 1.0f)
            {
                FillColor = fillColor;
                OutlineColor = outlineColor;
                OutlineThickness = outlineThickness;
            }

            public static DrawStyle FillOnly(Color fillColor)
            {
                return new DrawStyle(fillColor, Color.Transparent, 0);
            }

            public static DrawStyle OutlineOnly(Color outlineColor, float thickness)
            {
                return new DrawStyle(Color.Transparent, outlineColor, thickness);
            }
        }

        public static void Rect(SpriteBatch spriteBatch, Vector2 position, Dimensions size, DrawStyle style)
        {
            if (_pixelTexture == null)
            {
                throw new InvalidOperationException("DrawUtils.Initialize must be called before using DrawUtils.");
            }

            if (style.FillColor != Color.Transparent)
            {
                spriteBatch.Draw(_pixelTexture, new Rectangle((int)position.X, (int)position.Y, (int)size.Width, size.Height), style.FillColor);
            }

            // Outline
            if (style.OutlineColor != Color.Transparent && style.OutlineThickness > 0)
            {
                // Top
                spriteBatch.Draw(_pixelTexture, new Rectangle((int)position.X, (int)position.Y, size.Width, (int)style.OutlineThickness), style.OutlineColor);
                // Bottom
                spriteBatch.Draw(_pixelTexture, new Rectangle((int)position.X, (int)(position.Y + size.Height - style.OutlineThickness), size.Width, (int)style.OutlineThickness), style.OutlineColor);
                // Left
                spriteBatch.Draw(_pixelTexture, new Rectangle((int)position.X, (int)position.Y, (int)style.OutlineThickness, size.Height), style.OutlineColor);
                // Right
                spriteBatch.Draw(_pixelTexture, new Rectangle((int)(position.X + size.Width - style.OutlineThickness), (int)position.Y, (int)style.OutlineThickness, size.Height), style.OutlineColor);
            }
        }

        public static void Line(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            if (_pixelTexture == null)
            {
                throw new InvalidOperationException("DrawUtils.Initialize must be called before using DrawUtils.");
            }

            Vector2 direction = end - start;
            float length = direction.Length();
            float rotation = (float)Math.Atan2(direction.Y, direction.X);

            spriteBatch.Draw(
                _pixelTexture,
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
