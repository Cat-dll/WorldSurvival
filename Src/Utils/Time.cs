using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Utils
{
    public static class Time
    {
        public static ulong Frames { get; private set; }

        public static float Frametime { get; private set; }

        public static int FPS { get; private set; }

        public const int MAX_FPS = 10000;

        public static void Update(GameTime gameTime)
        {
            Frames++;
            Frametime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            FPS = (int)(MAX_FPS / Frametime / MAX_FPS);
        }
    }
}
