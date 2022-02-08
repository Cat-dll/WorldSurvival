using System;
using WorldSurvival.Gfx;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;

namespace WorldSurvival.Utils
{
    public static class Resources
    {
        private static ContentManager contentManager;

        public static string RESOURCES_PATH { get; private set; }  

        public static Texture2D WORLD_TEXTURE { get; private set; }

        public static Spritesheet WORLD_SPRITESHEET { get; private set; }

        public static SpriteFont GAME_FONT { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            contentManager = content;
            
            var workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            RESOURCES_PATH = Path.Combine(workDir, $"{content.RootDirectory}");

            // Texture
            WORLD_TEXTURE = contentManager.Load<Texture2D>("Environment");
            GAME_FONT = contentManager.Load<SpriteFont>("GameFont");

            // Spritesheet
            WORLD_SPRITESHEET = new Spritesheet(WORLD_TEXTURE, 16, 16);
        }

        public static T Load<T>(string path) where T : class
        {
            T animationRes = null;

            try
            {
                var fullPath = Path.GetFullPath($"{RESOURCES_PATH}\\{path}.json");

                StreamReader reader = new(File.Open(fullPath, FileMode.Open));
                
                string data = reader.ReadToEnd();
                animationRes = JsonSerializer.Deserialize<T>(data);

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] - Failure to load resources!\n\t");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                WorldSurvival.Close();
            }  

            return animationRes;
        }

        public static async Task<T> LoadAsync<T>(string path) where T : class
        {
            return await Task.Run(() =>
                Load<T>(path)
            );
        }

        public static void UnloadContent() => contentManager.Unload();
    }
}
