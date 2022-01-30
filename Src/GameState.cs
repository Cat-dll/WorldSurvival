using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSurvival.Entity;

namespace WorldSurvival
{
    public class GameState : IState
    {
        public EntityPlayer Player;

        public GameState()
        {
            this.Player = new EntityPlayer(new Vector2(GameSettings.GameWidth / 2.0f, GameSettings.GameHeight / 2.0f));
        }

        public void Tick()
        {
            Player.Tick();
        }

        public void Update()
        {
            Player.Update();
        }

        public void Render()
        {
            Player.Render();
        }
    }
}
