using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Gfx
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public Rectangle Bound
        {
            get
            {
                Rectangle bound = new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                return bound;
            }
        }

        private Vector2 _position = Vector2.Zero;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                translation = Matrix.CreateTranslation(new Vector3(-(int)value.X / Zoom, -(int)value.Y / Zoom, 0));
                Transform = translation * rotation * scaling;
            }
        }

        private float _rotation = 0.0f;
        public float Rotation
        {
            get => _rotation;
            set
            {
                rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(value));
                Transform = translation * rotation * scaling;
            }
        }

        private float _zoom = 1.0f;
        public float Zoom
        { 
            get => _zoom;
            set
            {
                _zoom = value;
                scaling = Matrix.CreateScale(value, value, 1);
                Transform = translation * rotation * scaling;
            }
        }

        private Vector2 Size;

        private Matrix translation;

        private Matrix rotation;

        private Matrix scaling;

        public Camera(Vector2 pPosition)
        {
            this.translation = Matrix.Identity;
            this.rotation = Matrix.Identity;
            this.scaling = Matrix.Identity;

            this.Position = pPosition;
            this.Size = new Vector2(GameSettings.WINDOW_WIDTH, GameSettings.WINDOW_HEIGHT);
        }

        public Camera(Vector2 pPosition, float pZoom) : this(pPosition)
        {
            this.Zoom = pZoom;
        }

        public Camera(Vector2 pPosition, float pZoom, float pRotation) : this(pPosition, pZoom)
        {
            this.Rotation = pRotation;
        }

        public Camera(Rectangle pBound, float pZoom, float pRotation) : this(new Vector2(pBound.X, pBound.Y), pZoom, pRotation)
        {
            this.Size.X = pBound.X;
            this.Size.Y = pBound.Y;
        }

        public void CenterOn(Vector2 pCenter)
        {
            Vector2 newPos = new(); 
            newPos.X = pCenter.X - (Size.X / 2);
            newPos.Y = pCenter.Y - (Size.Y / 2);
            this.Position = newPos;
        }
    }
}
