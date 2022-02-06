using WorldSurvival.Tile;
using System;

namespace WorldSurvival.Gfx
{
    public class Sprite
    {
        public Spritesheet Sheet { get; private set; }

        public int SpriteCoordX, SpriteCoordY;

        public Color Color;

        public Vector2 Scale;

        public float Rotation { get; set; }

        public SpriteEffects Effect { get; set; }

        
        public Sprite(Spritesheet spritesheet, int spriteCoordX, int spriteCoordY)
        {
            this.Sheet = spritesheet;
            this.SpriteCoordX = spriteCoordX;
            this.SpriteCoordY = spriteCoordY;

            this.Color = Color.White;
            this.Rotation = 0.0f;
            this.Scale = Vector2.One;
            this.Effect = SpriteEffects.None;

        }

        // TODO: Implement camera 
        public void Render(float posX, float posY) => WorldSurvival.Batch.Draw(
            Sheet.Source, 
            new Rectangle((int)posX, (int)posY, (int)(Sheet.SpriteWidth * Scale.X), (int)(Sheet.SpriteHeight * Scale.Y)), 
            Sheet.GetRectangleFrom(SpriteCoordX, SpriteCoordY),
            Color,
            Rotation,
            Vector2.Zero, 
            Effect, 
            1
        );
    }
}
