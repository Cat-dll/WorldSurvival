using System;

namespace WorldSurvival.Utils
{
    public struct Direction
    {
        public static readonly Direction North = new(0, -1);

        public static readonly Direction South = new(0, 1);

        public static readonly Direction West =  new(-1, 0);

        public static readonly Direction East =  new(1, 0);

        public int Dx { get; private set; } 
        
        public int Dy { get; private set; } 
        
        public Direction(int dx, int dy)
        {
            this.Dx = dx;
            this.Dy = dy;
        }

        public static Direction Get(Vector2 pos) => Get(pos.X, pos.Y);
        public static Direction Get(float x, float y)
        {
            if (Math.Abs(y) != 0)
                return y < 0 ? North : South;
            return x < 0 ? West : East;
        }

        public static bool operator ==(Direction a, Direction b) => a.Dx == b.Dx && a.Dy == b.Dy;
        
        public static bool operator !=(Direction a, Direction b) => a.Dx != b.Dx || a.Dy != b.Dy;
    }
}
