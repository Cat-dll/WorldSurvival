using WorldSurvival.Utils;
using WorldSurvival.Gfx;

namespace WorldSurvival.Entity
{
    public class EntityPlayer : Entity
    {
        #region Data
        private Animation animation;

        private Sprite sprite;

        public float Speed { get; set; } = 120;
        #endregion

        public EntityPlayer(Vector2 pos) : base(pos)
        {
            this.sprite = new Sprite(Resources.EnvironmentTilesheet, new Rectangle(18 * 16, 0, 48, 64), 0.0f, Color);
            sprite.Width = 64;
            sprite.Height = 64;

            this.animation = new Animation(sprite, Resources.PlayerWalkBottom, 0.2f);
        }

        #region State
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
        #endregion

        #region Events
        public override void OnDirectionChange(object sender)
        {
            if (Direction == Direction.East || Direction == Direction.West)
            {
                animation.CurrentAnimationData = Resources.PlayerWalkLeft;
                sprite.Effect = Direction == Direction.East ? SpriteEffects.FlipHorizontally : sprite.Effect;
            }

            if (Direction == Direction.North)
                animation.CurrentAnimationData = Resources.PlayerWalkTop;
            if (Direction == Direction.South)
                animation.CurrentAnimationData = Resources.PlayerWalkBottom;
        }

        public override void OnMove(object sender)
        {
            animation.Start();
            animation.Animate(true);
        }

        public override void OnIdle(object sender)
        {
            animation.Stop(true);
        }
        #endregion 

        #region Getter and Setters
        public Sprite GetSprite() => this.sprite;
        #endregion
    }
}
