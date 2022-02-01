using System;
using WorldSurvival.Utils;
using WorldSurvival.Tile;

namespace WorldSurvival.Gfx
{
    public class Animation
    {
        #region Data

        private AnimationData currentAnimationData;
        public AnimationData CurrentAnimationData
        {
            get => currentAnimationData;
            set
            {
                currentAnimationData = value;
                Indexes = 0;
                timer = 0.0f;
            }
        }

        public Sprite Sprite { get; set; }

        private int indexes;
        public int Indexes
        { 
            get => indexes;
            set
            {
                indexes = value;
                UpdateFrame();
            }
        }

        public float Delay { get; set; }

        public bool Pause { get; private set; }

        private float timer;
        #endregion

        public Animation(Sprite sprite, AnimationData animation, float delay)
        {
            this.Sprite = sprite;
            this.CurrentAnimationData = animation;
            this.Delay = delay;
            this.Pause = false;
            this.Indexes = 0;

            this.timer = 0.0f;
        }

        public void Animate(bool looping)
        {
            if (!Pause)
            {
                timer += Time.CurrentFrameTime;
                if (timer >= Delay)
                {
                    if (Indexes + 1 >= currentAnimationData.Length)
                    {
                        Pause = looping;
                        Indexes = 0;
                    }
                    else
                        Indexes++;

                    timer = 0.0f;
                }
            }
        }

        protected void UpdateFrame()
        {
            TileCoord coord = new(0, 0);
            coord.Tx = Indexes % currentAnimationData.Rows;
            coord.Ty = (int)MathF.Floor(Indexes / currentAnimationData.Rows);

            this.Sprite.SetRectangle(currentAnimationData.GetFrame(coord));
        }

        #region Getters and Setters
        public void Start() => Pause = false;
        public void Stop(bool reset)
        {
            if (!Pause)
            {
                Pause = true;
                Indexes = reset ? 0 : Indexes;
            }
        }
        #endregion
    }
}
