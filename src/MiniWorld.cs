using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishGame.Gfx;
using FishGame.Utils;

namespace FishGame
{
    public class MiniWorld : Game
    {
        public static MiniWorld Instance { get; private set; }

        public GraphicsDeviceManager GraphicsManager { get; private set; }

        // TODO: Implements Renderer.cs, no instance 
        public SpriteBatch Batch { get; private set; } = null!;

        public Texture2D Texture { get; private set; } = null!;

        public IState CurrentState = null!;

        public Camera mainCamera = null!;

        private int tickTimer = 0;

        public MiniWorld()
        {
            Instance = this;

            this.GraphicsManager = new GraphicsDeviceManager(this);
            this.GraphicsManager.PreferredBackBufferWidth = GameSettings.WINDOW_WIDTH;
            this.GraphicsManager.PreferredBackBufferHeight = GameSettings.WINDOW_HEIGHT;
            this.GraphicsManager.PreferHalfPixelOffset = false;

            Content.RootDirectory = "Resources/";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            this.Window.AllowAltF4 = false;
            this.Window.AllowUserResizing = false;
            this.Window.Title = GameSettings.WINDOW_TITLE;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            Texture = Content.Load<Texture2D>("Texture/tileset");
            mainCamera = new Camera(Vector2.Zero, 4);
            mainCamera.CenterOn(Vector2.Zero);

            CurrentState = new GameState();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Time.Update(gameTime);

            tickTimer++;
            if (tickTimer >= GameSettings.GAME_TICKS)
            {
                CurrentState.Tick();
                tickTimer = 0;
            }

            CurrentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            Batch.Begin(samplerState:SamplerState.PointClamp, transformMatrix:mainCamera.Transform);
            CurrentState.Render();
            Batch.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
