
namespace WorldSurvival.Gfx
{
    public class Sprite
    {
        public Texture2D Texture;

        public Rectangle Source;

        public Color Color;

        public int Width { get; set; }

        public int Height { get; set; }

        public float Rotation { get; set; }

        public Sprite(Texture2D texture) : this(texture, Color.White) { /* Void */ }

        public Sprite(Texture2D texture, Color color) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height), 0.0f, color) { /* Void */ }
        public Sprite(Texture2D texture, Rectangle source, float rotation, Color color)
        {
            Texture = texture;
            Source = source;
            Color = color;

            Rotation = rotation;
            Width = texture.Width;
            Height = texture.Height;
        }

        public void Render(float posX, float posY) => Batch.Draw(
            Texture, 
            new Rectangle((int)posX, (int)posY, Source.Width * 3, Source.Height * 3), 
            Source,
            Color,
            Rotation,
            Vector2.Zero, 
            SpriteEffects.None, 
            1
        );
    }
}
