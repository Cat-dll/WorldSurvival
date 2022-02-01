using System;
using WorldSurvival.Utils;

namespace WorldSurvival.Entity
{
    public class Entity
    {
        #region Data
        public static int EntitiesInGame { get; protected set; } = 0;

        protected Vector2 LastPosition;
        
        protected Vector2 Position;

        public Color Color;

        protected Direction LastDirection;
        
        public Direction Direction;
        #endregion

        public delegate void EntityEventHandler(object sender);
        
        public event EntityEventHandler DirectionChangeEvent;

        public event EntityEventHandler MoveEvent;

        public event EntityEventHandler IdleEvent;

        // TODO: Implement swimming
        //public event EventHandler OnSwim;
        
        public Entity(Vector2 position)
        {
            this.Position = position;
            this.LastPosition = this.Position;

            this.Direction = Direction.South;
            this.LastDirection = new Direction();
            this.Color = Color.White;

            this.DirectionChangeEvent += OnDirectionChange;
            this.MoveEvent += OnMove;
            this.IdleEvent += OnIdle;
            
            EntitiesInGame++;
        }

        #region State
        public virtual void Tick()
        {
            float dx = Position.X - LastPosition.X, dy = Position.Y - LastPosition.Y;
            if (dx != 0 || dy != 0)
                this.Direction = Direction.Get(dx, dy);

            if (LastDirection != Direction)
                DirectionChangeEvent?.Invoke(this);
            if (LastPosition != Position)
                MoveEvent?.Invoke(this);
            else
                IdleEvent?.Invoke(this);

            this.LastPosition = Position;
            this.LastDirection = Direction;
        }

        public virtual void Render() { /* VOID */ }

        public virtual void Update() { /* VOID */ }
        #endregion

        #region Utility Methods
        public void Move(float vx, float vy)
        {
            if (vx == 0 && vy == 0) 
                return;
            
            Position.X += (vx * Time.CurrentFrameTime);
            Position.Y += (vy * Time.CurrentFrameTime);
        }
        #endregion

        #region Events
        public virtual void OnDirectionChange(object sender) { }
        public virtual void OnMove(object sender) { }

        public virtual void OnIdle(object sender) { }
        #endregion

        #region Getters and Setters
        public Vector2 GetPosition() => this.Position;
        public static string GetDefaultEntityName() => $"Entity_{EntitiesInGame}";
        #endregion
    }
}
