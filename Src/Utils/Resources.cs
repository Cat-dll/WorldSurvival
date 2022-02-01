using System;
using WorldSurvival.Gfx;
using WorldSurvival.Tile;

namespace WorldSurvival.Utils
{
    public static class Resources
    {
        private static ContentManager contentManager;

        public static Texture2D EnvironmentTilesheet { get; private set; }

        public static SpriteFont GameFont { get; private set; }

        public static AnimationData PlayerWalkBottom { get; private set; }
        public static AnimationData PlayerWalkTop { get; private set; }
        public static AnimationData PlayerWalkLeft { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            contentManager = content ?? throw new ArgumentNullException(nameof(content)); 

            // Texture
            EnvironmentTilesheet = contentManager.Load<Texture2D>("Environment");
            GameFont = contentManager.Load<SpriteFont>("GameFont");

            // Animation
            PlayerWalkBottom = new AnimationData(new(0, 0), new(2, 0), 16, 16); // TODO: Using another alternative for animation resources.
            PlayerWalkTop = new AnimationData(new(0, 3), new(2, 3), 16, 16);
            PlayerWalkLeft = new AnimationData(new(0, 1), new(2, 1), 16, 16);
        }

        public static void UnloadContent() => contentManager.Unload();
    }
}
