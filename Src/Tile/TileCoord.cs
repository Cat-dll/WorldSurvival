using System;
using WorldSurvival.Utils;

namespace WorldSurvival.Tile
{
    public struct TileCoord : ICoord
    {
        public int Tx, Ty;

        public TileCoord(int tx, int ty)
        {
            this.Tx = tx;
            this.Ty = ty;
        }

        // TODO: Implements other coordinate
        //public ChunkCoord GetChunkCoord();
        //public TileCoord GetLocalTileCoord();
        public Vector2 GetPixel() => new(Tx * 16, Ty * 16); // TODO: Implements tile data and other stuff related

        public static TileCoord operator +(TileCoord a, TileCoord b) => new(a.Tx + b.Tx, b.Tx + b.Ty);
        public static TileCoord operator -(TileCoord a, TileCoord b) => new(a.Tx - b.Tx, a.Ty - b.Ty);
        public static TileCoord operator *(TileCoord a, TileCoord b) => new(a.Tx * b.Tx, a.Ty * b.Ty);
        public static TileCoord operator /(TileCoord a, TileCoord b) => new(a.Tx / b.Tx, a.Ty / b.Ty);
        public static TileCoord operator %(TileCoord a, TileCoord b) => new(a.Tx % b.Tx, a.Ty % b.Ty);
    }
}
