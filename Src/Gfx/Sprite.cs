using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Gfx
{
    public class Sprite
    {
        public Texture2D Texture { get; protected set; }

        public Rectangle source;

        public Color color;

        public int ScaleX { get; set; }
        
        public int ScaleY { get; set; }

        public float Rotation { get; set; }

        public SpriteEffects Effects { get; set; }

        public Sprite(Texture2D pTexture, Color pColor)
        {
            this.Texture = pTexture;
            this.color = pColor;

            this.source = pTexture.Bounds;
            this.ScaleX = 1;
            this.ScaleY = 1;
            this.Rotation = 0.0f;
        }

        public Sprite(Texture2D pTexture, Rectangle pSource, Color pColor) : this(pTexture, pColor)
        {
            if (pSource.Y < pTexture.Width && pSource.Y < pTexture.Height)
                this.source = pSource;
        }

        public void Render(float pX, float pY)
        {
            MiniWorld.Instance.Batch.Draw(Texture, 
                new(pX, pY),                 // Position
                source,                      // Texture source
                color,                       // Sprite color
                Rotation,                    // Sprite rotation
                Vector2.Zero,                // Sprite draw origin
                new Vector2(ScaleX, ScaleY), // Sprite scaling
                Effects,                     // Sprite effects
                1                            // Sprite Depth
            );
        }
    }
}
