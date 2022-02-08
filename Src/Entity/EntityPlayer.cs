using WorldSurvival.Utils;
using WorldSurvival.Gfx;

namespace WorldSurvival.Entity
{
    public class EntityPlayer : Entity
    {
        protected Sprite sprite;

        public float Speed { get; set; } = 120;

        public EntityPlayer(Vector2 pos) : base(pos)
        {
            this.sprite = new Sprite(Resources.WORLD_SPRITESHEET, 8, 5) { 
                Scale = Vector2.One * 2, // (2, 2)
                Color = Color.Blue
            };
        }

        public override void Tick()
        {
            base.Tick();

            // TODO: Using a control system instead of simple key down
            if (Input.IsKeyDown(Keys.W))
                this.Move(0, -Speed);
            if (Input.IsKeyDown(Keys.A))
                this.Move(-Speed, 0);
            if (Input.IsKeyDown(Keys.S))
                this.Move(0, Speed);
            if (Input.IsKeyDown(Keys.D))
                this.Move(Speed, 0);
        }

        public override void Render() => this.sprite.Render(Position.X, Position.Y);

       // public override void OnDirectionChange(object sender)
      //  {

       // }

       // public override void OnMove(object sender)
       // {

      //  }

       // public override void OnIdle(object sender)
       // {

      //  }

        public Sprite GetSprite() => sprite;
    }
}
