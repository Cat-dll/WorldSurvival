using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishGame.Gfx;

namespace FishGame.Entity
{
    public class EntityPlayer : Entity
    {
       // public Sprite PlayerSprite { get; protected set; }

        public float Velocity { get; set; }

        public EntityPlayer(string pName) : base(pName)
        {
          //  this.PlayerSprite = AssetsManager.GAME_TEXTURES.GetSprite(2, 2);
            this.Velocity = 10.0f;
        }
    }
}
