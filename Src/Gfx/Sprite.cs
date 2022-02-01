
namespace WorldSurvival.Gfx
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }

        private Rectangle source;

        public Color Color;

        public int Width { get; set; }

        public int Height { get; set; }

        public float Rotation { get; set; }

        public SpriteEffects Effect { get; set; }

        private Rectangle rect;

        public Sprite(Texture2D texture) : this(texture, Color.White) { /* Void */ }
        public Sprite(Texture2D texture, Color color) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height), 0.0f, color) { /* Void */ }
        public Sprite(Texture2D texture, Rectangle source, float rotation, Color color)
        {
            this.source = source;
            this.rect = source;

            Texture = texture;
            Color = color;

            Rotation = rotation;
            Width = rect.Width;
            Height = rect.Height;
            Effect = SpriteEffects.None;
        }

        // TODO: Implement camera 
        public void Render(float posX, float posY) => Batch.Draw(
            Texture, 
            new Rectangle((int)posX, (int)posY, Width, Height), 
            rect,
            Color,
            Rotation,
            Vector2.Zero, 
            Effect, 
            1
        );

        #region Getters and Setters
        public void SetRectangle(Rectangle rectangle)
        {
            rect = new(source.X + rectangle.X, source.Y + rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public Rectangle GetRectangle(Rectangle rectangle) => new(rectangle.X - source.X, rectangle.Y - source.Y, rectangle.Width, rectangle.Height);
        #endregion
    }
}
