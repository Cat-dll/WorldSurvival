using System;
using WorldSurvival.Gfx.Animation;
using WorldSurvival.Gfx;
using WorldSurvival.Tile;

namespace WorldSurvival.Utils
{
    public static class Resources
    {
        private static ContentManager contentManager;

        public static Texture2D WORLD_TEXTURE { get; private set; }

        public static Spritesheet WORLD_SPRITESHEET { get; private set; }

        public static SpriteFont GAME_FONT { get; private set; }

        public static AnimationData PLAYER_ANIMATION { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            contentManager = content;

            // Texture
            WORLD_TEXTURE = contentManager.Load<Texture2D>("Environment");
            WORLD_SPRITESHEET = new Spritesheet(WORLD_TEXTURE, 16, 16);
            GAME_FONT = contentManager.Load<SpriteFont>("GameFont");

            // TODO: Implements animation resources
            PLAYER_ANIMATION = new AnimationData();
            PLAYER_ANIMATION.AddAnimation("PlayerWlkSth", new Animation(new Vector2(3, 1), 18, 0));
            PLAYER_ANIMATION.AddAnimation("PlayerWlkNth", new Animation(new Vector2(3, 1), 18, 3));
            PLAYER_ANIMATION.AddAnimation("PlayerWlkWst", new Animation(new Vector2(3, 1), 18, 1));
        }

        public static void UnloadContent() => contentManager.Unload();
    }
}
