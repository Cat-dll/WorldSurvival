using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public interface IState
    {
        public void Tick();

        public void Update();

        public void Render();
    }
}
