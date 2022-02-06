using System;
using WorldSurvival.Tile;

namespace WorldSurvival.Gfx
{
    public class Spritesheet
    {
        public Texture2D Source { get; private set; }

        public int SpriteWidth { get; set; }

        public int SpriteHeight { get; set; }

        // NOTE: The width and height is in sprite size not pixel
        public int Width { get; set; }

        public int Height { get; set; }

        public Spritesheet(Texture2D source, int spriteWidth, int spriteHeight)
        {
            this.Source = source;
            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = spriteHeight;
            
            this.Width = source.Width / SpriteWidth;
            this.Height = source.Height / SpriteHeight;
        }

        public Rectangle GetRectangleFrom(int spriteCoordX, int spriteCoordY) => new(spriteCoordX * SpriteWidth, spriteCoordY * SpriteHeight, SpriteWidth, SpriteHeight);
    }
}