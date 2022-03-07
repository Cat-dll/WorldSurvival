using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Utils
{
    public struct Direction
    {
        public static Direction NONE { get; private set; }  = new(0, 0);
        public static Direction EAST { get; private set; }  = new(1,  0);
        public static Direction WEST { get; private set; }  = new(-1, 0);
        public static Direction NORTH { get; private set; } = new(0, -1);
        public static Direction SOUTH { get; private set; } = new(0,  1);

        public int Dx { get; set; }

        public int Dy { get; set; }

        public Direction(int pDirX, int pDirY)
        {
            this.Dx = pDirX;
            this.Dy = pDirY;
        }

        public static Direction GetDirection(Vector2 position) => GetDirection(new Point((int)position.X, (int)position.Y));

        public static Direction GetDirection(Point position)
        {
            if (Math.Abs(position.Y) != 0)
                return position.Y > 0 ? SOUTH : NORTH;

            return position.X > 0 ? EAST : WEST;
        }

        public static implicit operator Point(Direction d) => new(d.Dx, d.Dy);

        public static implicit operator Vector2(Direction d) => new(d.Dx, d.Dy);
    }
}
