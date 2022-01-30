global using static WorldSurvival.WorldSurvival;

global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Input;

using WorldSurvival.Utils;

namespace WorldSurvival
{
    public class WorldSurvival : Game
    {
        public static IState CurrentState { get; private set; }

        public static GraphicsDeviceManager GraphicsManager { get; private set; }

        public static SpriteBatch Batch { get; private set; }

        private float _tpsTimer = 0;

        public WorldSurvival()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/bin/Windows/Content";

            this.IsMouseVisible = true;
            this.Window.AllowAltF4 = false;
            this.Window.AllowUserResizing = false;
            this.Window.Title = GameSettings.GameTitle;
            GraphicsManager.PreferredBackBufferWidth = GameSettings.GameWidth;
            GraphicsManager.PreferredBackBufferHeight = GameSettings.GameHeight;
            GraphicsManager.PreferHalfPixelOffset = false;
            this._tpsTimer = 0;
        }

        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            Resources.LoadContent(this.Content);

            CurrentState = new GameState();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Calculate(gameTime);
            Input.Update();
            
            _tpsTimer += Time.CurrentFrameTime;
            if (_tpsTimer > 1.0f / Time.Tps)
            {
                CurrentState.Tick();
                _tpsTimer = 0;
            }

            CurrentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            Batch.Begin(samplerState: SamplerState.PointClamp);
            CurrentState.Render();
            Batch.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            Resources.UnloadContent();

            base.UnloadContent();
        }
    }
}
