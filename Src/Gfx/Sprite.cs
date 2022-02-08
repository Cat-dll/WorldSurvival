using WorldSurvival.Tile;
using System;

namespace WorldSurvival.Gfx
{
    public class Sprite
    {
        public Spritesheet Sheet { get; set; }

        public int FrameX { get; set; }

        public int FrameY { get; set; }

        // NOTE: (Je ne suis pas capable de trouver d'autre nom) 
        public int FrameWidth { get; set; } // NOTE: Représente la taille en tile/frame que le sprite utilise (ET non la taille en pixel des frames)

        public int FrameHeight { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }

        public Color Color { get; set; }

        public SpriteEffects Effect { get; set; }

        public Sprite(Spritesheet pSheet, int pFrameX, int pFrameY)
        {
            this.Sheet = pSheet;
            this.FrameX = pFrameX;
            this.FrameY = pFrameY;
            this.FrameWidth = 1;
            this.FrameHeight = 1;

            this.Rotation = 0.0f;
            this.Scale = Vector2.One;
            this.Color = Color.White;
            this.Effect = SpriteEffects.None;
        }

        public void Render(float pX, float pY) => WorldSurvival.Batch.Draw(
            Sheet.Source,
            new Vector2(pX, pY),
            new Rectangle(FrameX * Sheet.SpriteWidth, FrameY * Sheet.SpriteHeight, FrameWidth * Sheet.SpriteWidth, FrameHeight * Sheet.SpriteHeight),
            Color,
            Rotation,
            Vector2.Zero,
            Scale,
            Effect,
            1
        );
    }
}
