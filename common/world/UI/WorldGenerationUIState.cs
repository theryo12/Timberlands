using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Timberlands.Utils;

namespace Timberlands.Common.World.UI;

public class WorldGenerationUIState : UIState
{
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        DrawUtils.Primitives.Rect(spriteBatch, Vector2.Zero, new Dimensions((ushort)Main.screenWidth, (ushort)Main.screenHeight), DrawUtils.Primitives.DrawStyle.FillOnly(Color.Black));

        var rand = new Random();
        var block = new WorldBlock(
            id: WorldBlock.GenerateID("NewBlock", 123),
            type: WorldBlock.BlockType.Static,
            minSize: new Dimensions(300, 300),
            maxSize: new Dimensions(500, 500),
            position: Vector2.Zero
        );
        block.Draw(ref rand, Main.spriteBatch, DrawUtils.Primitives.DrawStyle.OutlineOnly(Color.AntiqueWhite, 5f), Vector2.Zero);
    }
}