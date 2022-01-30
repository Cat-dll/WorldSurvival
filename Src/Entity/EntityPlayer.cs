using WorldSurvival.Utils;
using WorldSurvival.Gfx;

namespace WorldSurvival.Entity
{
    public class EntityPlayer : Entity
    {
        #region Data
        private Animation _animation;

        private Sprite _sprite;

        public float Speed { get; set; } = 120;
        #endregion

        public EntityPlayer(Vector2 pos) : base(pos)
        {
            this._sprite = new Sprite(Resources.EnvironmentTilesheet, Color);
            this._animation = new Animation(_sprite, 16, 3, 18 / Speed)
            {
                OffsetX = 18 * 16, // 19 tile offset
                OffsetY = 0
            };
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

        public override void Render() => this._sprite.Render(Position.X, Position.Y);
        #endregion

        #region Events
        public override void OnDirectionChange(object sender)
        {
            // TODO: Using a animation state machine or other system
            if (this.Direction == Direction.West || this.Direction == Direction.East)
                _animation.OffsetY = this.Direction == Direction.West ? 16 : 32;
            else if (this.Direction == Direction.North || this.Direction == Direction.South)
                _animation.OffsetY = this.Direction == Direction.South ? 0 : 48;
        }

        public override void OnMove(object sender)
        {
            _animation.Animate(true);
        }

        public override void OnIdle(object sender)
        {
            _animation.Reset();
        }
        #endregion 

        #region Getter and Setters
        public Sprite GetSprite() => this._sprite;
        #endregion
    }
}
