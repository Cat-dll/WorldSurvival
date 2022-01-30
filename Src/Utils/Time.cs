using System;
using System.Globalization;

namespace WorldSurvival.Utils
{
    public static class Time
    {
        public static DateTime Now { get; private set; }

        public static int Frames { get; private set; } = 0;

        public static float CurrentFrameTime { get; private set; } = 0;

        public static int CurrentFps { get; private set; } = 0;

        public static int Tps { get; private set; } = 60;

        //public int FPS { get; private set; }


        public static void Calculate(GameTime gameTime)
        {
            Now = DateTime.Now;
            CurrentFrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CurrentFps = (int)Math.Round(1.0f / CurrentFrameTime);

            Frames++;
        }
    }
}