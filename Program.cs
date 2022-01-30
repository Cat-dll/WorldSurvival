using System;

namespace WorldSurvival
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new WorldSurvival())
                game.Run();
        }
    }
}
