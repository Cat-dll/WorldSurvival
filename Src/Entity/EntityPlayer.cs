using WorldSurvival.Utils;
using WorldSurvival.Gfx;
using WorldSurvival.Gfx.Animation;

namespace WorldSurvival.Entity
{
    public class EntityPlayer : Entity
    {
        private AnimationPlayer animation;

        private Sprite sprite;

        public float Speed { get; set; } = 120;

        public EntityPlayer(Vector2 pos) : base(pos)
        {
            this.sprite = new Sprite(Resources.WORLD_SPRITESHEET, 0, 0);
            sprite.Scale = new Vector2(2, 2);

            this.animation = new AnimationPlayer(sprite, Resources.PLAYER_ANIMATION, 0.2f);
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

        public override void OnDirectionChange(object sender)
        {
            if (Direction == Direction.East || Direction == Direction.West)
            {
                animation.Play("PlayerWlkWst");
                sprite.Effect = Direction == Direction.East ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }

            if (Direction == Direction.North)
                animation.Play("PlayerWlkNth");
            if (Direction == Direction.South)
                animation.Play("PlayerWlkSth");
        }

        public override void OnMove(object sender)
        {
            animation.Animate(true);
        }

        public override void OnIdle(object sender)
        {
            animation.Reset();
        }

        public Sprite GetSprite() => sprite;
    }
}
