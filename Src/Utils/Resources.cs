using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSurvival.Utils
{
    public static class Resources
    {
        private static ContentManager _contentManager;

        public static Texture2D EnvironmentTilesheet { get; private set; }

        public static SpriteFont GameFont { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            _contentManager = content ?? throw new ArgumentNullException(nameof(content)); 

            EnvironmentTilesheet = _contentManager.Load<Texture2D>("Environment");
            GameFont = _contentManager.Load<SpriteFont>("GameFont");
        }

        // Unload every assets
        public static void UnloadContent() => _contentManager.Unload();
    }
}
