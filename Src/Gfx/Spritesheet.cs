using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Gfx
{
    public class Spritesheet : Sprite
    {
        public int SpriteWidth { get; set; }

        public int SpriteHeight { get; set; }

        public int WidthInSprite { get; protected set; }

        public int HeightInSprite { get; protected set; }

        public Spritesheet(Texture2D pSource, int pSpriteWidth, int pSpriteHeight) : base(pSource, Color.White)
        {
            this.SpriteWidth = pSpriteWidth;
            this.SpriteHeight = pSpriteHeight;
            this.WidthInSprite = (source.Width / SpriteWidth);
            this.HeightInSprite = (source.Height / SpriteHeight);
        }

        public Sprite GetSprite(int pTx, int pTy)
        {
            Sprite sprite = new(this.Texture, this.color)
            {
                Rotation = this.Rotation,
                ScaleX = this.ScaleX,
                ScaleY = this.ScaleY,
                Effects = this.Effects
            };

            if (pTx <= WidthInSprite && pTy <= HeightInSprite)
            {
                sprite.source.Location = new Point(pTx * 8, pTy * 8); // TODO: Remplace by Tile size constant
                sprite.source.Size = new Point(SpriteWidth, SpriteHeight);
            }

            return sprite;
        }

    }
}
