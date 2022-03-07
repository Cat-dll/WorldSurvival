using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishGame.Utils;

namespace FishGame.Entity
{
    public class Entity
    {
        public string Name { get; set; }


        public float PreviousPosX { get; protected set; }

        public float PreviousPosY { get; protected set; }

        public float PosX { get; set; }

        public float PosY { get; set; }


        protected Direction previousDirection;
        
        public Direction Direction { get; set; }

        public bool IsMoving { get; protected set; }

        public Entity(string pName)
        {
            this.Name = pName;

            this.PreviousPosX = 0;
            this.PreviousPosY = 0;
            this.PosX = PreviousPosX;
            this.PosY = PreviousPosY;

            this.previousDirection = Direction.NONE;
            this.Direction = previousDirection;
        }

        public virtual void Tick()
        {
            previousDirection = this.Direction;

            Vector2 posDiff = new(PosX - PreviousPosX, PosY - PreviousPosY);
            this.IsMoving = posDiff != Vector2.Zero;
            this.Direction = Direction.GetDirection(posDiff);
        }

        public virtual void Update()
        { 
            
        }

        public virtual void Render()
        { 
        
        }

        public void Move(float velocity)
        {
            if (velocity != 0)
            {
                this.PosX += this.Direction.Dx * (velocity * Time.Frametime);
                this.PosY += this.Direction.Dy * (velocity * Time.Frametime);
            }
            
            // NOTE: Manage collision and other things
        }
    }
}    
