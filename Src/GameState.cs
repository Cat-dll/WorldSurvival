using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishGame.Gfx;

namespace FishGame
{
    public class GameState : IState
    {
        public Sprite PlayerSprite;

        public GameState()
        {
            this.PlayerSprite = new Spritesheet(MiniWorld.Instance.Texture, 8, 8).GetSprite(2, 2);
        }

        public void Tick()
        {

        }
         
        public void Update()
        {

        }

        public void Render()
        {
            PlayerSprite.Render(3, 3);
        }
    }
}
