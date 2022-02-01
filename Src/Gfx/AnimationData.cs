using System;
using WorldSurvival.Tile;
using WorldSurvival.Utils;
using System.Threading.Tasks;
using System.IO;

namespace WorldSurvival.Gfx
{
    public class AnimationData
    {
        public int FrameWidth { get; private set; }

        public int FrameHeight { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public TileCoord StartFrame { get; private set; }

        public TileCoord EndFrame { get; private set; }

        public int Length { get; private set; }

        public AnimationData(TileCoord firstFrame, TileCoord lastFrame, int frameWidth, int frameHeight)
        {
            this.StartFrame = firstFrame;
            this.EndFrame = lastFrame;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;

            this.Rows = Math.Abs(EndFrame.Tx - StartFrame.Tx) + 1;
            this.Columns = Math.Abs(EndFrame.Ty - StartFrame.Ty) + 1;

            this.Length = Rows * Columns;
        }

        public Rectangle GetFrame(TileCoord coord) => new((StartFrame.Tx + coord.Tx) * FrameWidth, (StartFrame.Ty + coord.Ty) * FrameHeight, FrameWidth, FrameHeight);
    }
}



/*public void Load(string path)
{

}

public async Task<IResources> LoadAsync(string path)
{
    return new Task<IResources>(() =>
    {
        try
        {
            using (StreamReader reader = new(File.Open(path, FileMode.Open)))
            {

            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.StackTrace);
            Console.Error.WriteLine("\n{0}", ex.Message);
            GameExit = true;
        }
    });
}*/