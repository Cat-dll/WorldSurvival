using WorldSurvival.Utils;
using System.Diagnostics;
using System;

namespace WorldSurvival.Gfx.Animation
{
    public class AnimationPlayer
    {
        public Sprite CurrentSprite { get; set; }

        public AnimationData CurrentAnimationData { get; set; }

        public Animation CurrentAnimation { get; private set; }

        public bool Pause { get; set; }

        public float Delay { get; set; }

        protected bool stop;

        protected float timer;

        protected int frameIndexX;

        protected int frameIndexY;

        public int FrameIndexX
        { 
            get => frameIndexX;
            set
            {
                if (value >= 0 & value <= CurrentAnimation.Size.X)
                {
                    CurrentSprite.SpriteCoordX = CurrentAnimation.OffsetX + frameIndexX;
                    frameIndexX = value;
                    timer = 0.0f;
                }
            }
        }

        public int FrameIndexY
        { 
            get => frameIndexY;
            set
            {
                if (value >= 0 & value <= CurrentAnimation.Size.Y)
                {
                    CurrentSprite.SpriteCoordY = CurrentAnimation.OffsetY + frameIndexY;
                    frameIndexY = value;
                    timer = 0.0f;
                }
            }
        }

        public AnimationPlayer(Sprite sprite, AnimationData animationData, float delay)
        {
            this.CurrentSprite = sprite;
            this.CurrentAnimationData = animationData;
            this.CurrentAnimation = animationData.GetAnimation();
            this.Delay = delay;

            this.Reset();

            this.timer = 0.0f;
        }

        public void Play(string name)
        {
            CurrentAnimation = CurrentAnimationData.GetAnimation(name);
            this.Reset();
        }

        public void Animate(bool loop)
        {
            if (Pause)
                return;

            timer += Time.CurrentFrameTime;
            if (timer >= Delay)
            {
                if (FrameIndexX + 1 < CurrentAnimation.Size.X)
                    FrameIndexX++;
                else if (FrameIndexY + 1 < CurrentAnimation.Size.Y)
                    FrameIndexY++;
                else if (loop)
                    this.Reset();
                else
                    this.Stop();
            }
        }

        public void Reset()
        {
            FrameIndexX = 0;
            FrameIndexY = 0;
        }

        public void Stop()
        {
            if (!Pause)
            {
                Pause = true;
                this.Reset();
            }
        }
    }
}
