
namespace WorldSurvival.Gfx.Animation
{   
    public class Animation
    {
        public int OffsetX { get; private set; }

        public int OffsetY { get; private set; }

        public Vector2 FrameSize { get; private set; }

        public Vector2 Size { get; private set; }

        public Animation(Vector2 size, int offsetX, int offsetY)
        {
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
            this.FrameSize = new(16, 16); // TODO: Remove hard coded value for Tile.SIZE;
            this.Size = size;
        }
    }
}