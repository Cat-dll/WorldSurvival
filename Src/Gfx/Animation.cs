using System;
using WorldSurvival.Utils;
namespace WorldSurvival.Gfx
{
    // TODO: Rework animation
    public class Animation
    {
        #region Data
        public Sprite _sprite { get; private set; }

        public int FrameSize { get; set; }

        public int Length { get; set; }

        private int _offsetX;
        public int OffsetX
        {
            get => _offsetX;
            set
            {
                _offsetX = value;
                Reset();
            }
        }

        private int _offsetY;
        public int OffsetY
        {
            get => _offsetY;
            set
            {
                _offsetY = value;
                Reset();
            }
        }

        public float Delay { get; set; }

        public Direction Direction; 

        public int Indexes { get; private set; }

        private float _timer;

        private Rectangle _currentFrame;

        private bool _loopFinish;

        #endregion

        public Animation(Sprite source, int frameSize, int animationSize, float delay) : this(source, frameSize, animationSize, delay, Direction.East) { }

        public Animation(Sprite source, int frameSize, int animationSize, float delay, Direction animationDirection)
        {
            FrameSize = frameSize;
            Length = animationSize;
            Delay = delay;

            Indexes = 0;
            Direction = animationDirection;

            _sprite = source;
            _offsetX = 0;
            _offsetY = 0;
            _timer = 0;
            _currentFrame = new Rectangle(0, 0, FrameSize, FrameSize);
            _loopFinish = false;
        }

        public void Animate(bool looping)
        {
            if (_loopFinish)
                return;

            _timer += Time.CurrentFrameTime;
            if (_timer >= Delay)
            {
                Indexes++;
                if (Indexes >= Length)
                {
                    if (looping)
                        Reset();
                    else
                        _loopFinish = true;
                }
                else
                    SetFrame(Indexes);

                _timer = 0;
            }
        }

        public void Reset()
        {
            Indexes = 0;
            _currentFrame.X = _offsetX;
            _currentFrame.Y = _offsetY;
            _sprite.Source = _currentFrame;
        }

        public void SetFrame(int index)
        {
            var x = Direction.Dx * index * FrameSize + _offsetX;
            var y = Direction.Dy * index * FrameSize + _offsetY;

            // NOTE: Collision ??
            if (x + FrameSize > _sprite.Width && y + FrameSize > _sprite.Height && x < 0 && y < 0)
                throw new ArgumentException("Animation frame is out of the sprite size!");

            _currentFrame.X = x;
            _currentFrame.Y = y;
            _sprite.Source = _currentFrame;

            Indexes = index;
        }

        #region Getters and Setters
        #endregion
    }
}