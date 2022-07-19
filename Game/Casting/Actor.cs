using System;
using System.Numerics;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    
    /// <summary>
    /// A participant in the game.
    /// </summary>
    public class Actor
    {

        private bool _enabled = true;
        private Vector2 _position = Vector2.Zero;
        private float _rotation = 0f;
        private float _scale = 1f;
        private Vector2 _size = Vector2.Zero;
        private Color _tint = Color.White();
        private Vector2 _velocity = Vector2.Zero;
        

        public Actor() { }


        public virtual void ClampTo(Actor region)
        {
            Validator.CheckNotNull(region);
            
            if (_enabled)
            {
                float x = GetLeft();
                float y = GetTop();

                float maxX = region.GetRight() - GetWidth();
                float maxY = region.GetBottom() - GetHeight();
                float minX = region.GetLeft();
                float minY = region.GetTop();

                x = Math.Clamp(x, minX, maxX);
                y = Math.Clamp(y, minY, maxY);

                Vector2 newPosition = new Vector2(x, y);
                MoveTo(newPosition);
            }
        }


        public int DetectCollisionDirection(Actor platform)
        {
            int collisionDirection = 0;
            
            if (this.GetRight() - 5 > platform.GetLeft() && 
            this.GetLeft() + 5 < platform.GetRight() && 
            this.GetTop() < platform.GetBottom())
            {
                // Player collides with bottom of platform
                collisionDirection = 1;
            }
            // if (this.GetRight() < platform.GetLeft() && this.GetLeft() < platform.GetLeft() - this.GetWidth() + 3 && this.GetBottom() > platform.GetTop() && this.GetTop() < platform.GetBottom())
            else if (this.GetRight() > platform.GetLeft() && 
            this.GetLeft() < platform.GetRight() - this.GetWidth() / 2 && 
            (this.GetBottom() > platform.GetTop() || this.GetTop() < platform.GetBottom()))
            {
                collisionDirection = 2;
            }
            // else if (this.GetLeft() > platform.GetRight() && this.GetRight() > platform.GetRight() + this.GetWidth() + 3 && this.GetBottom() > platform.GetTop() && this.GetTop() < platform.GetBottom())
            else if (this.GetLeft() < platform.GetRight() && 
            this.GetRight() > platform.GetLeft() + this.GetWidth() / 2 && 
            (this.GetBottom() > platform.GetTop() || this.GetTop() < platform.GetBottom()))
            {
                // Player collides with right of platform
                collisionDirection = 3;
            }
            // if (this.GetBottom() < platform.GetTop() && this.GetTop() < platform.GetTop() - this.GetHeight() - 3 && this.GetLeft() < platform.GetRight() && this.GetRight() > platform.GetLeft())
            if (this.GetRight() - 5 > platform.GetLeft() && 
            this.GetLeft() + 5 < platform.GetRight() && 
            this.GetBottom() >= platform.GetTop() && 
            this.GetTop() < platform.GetTop())
            {
                // Player collides with top of platform
                collisionDirection = 4;
            }

            return collisionDirection;
        }


        public virtual float GetBottom()
        {
            return _position.Y + _size.Y;
        }


        public virtual Vector2 GetCenter()
        {
            float x = _position.X + (_size.X / 2);
            float y = _position.Y + (_size.Y / 2);
            return new Vector2(x, y);
        }


        public virtual float GetHeight()
        {
            return _size.Y;
        }


        public virtual float GetLeft()
        {
            return _position.X;
        }


        public virtual Vector2 GetPosition()
        {
            return _position;
        }


        public virtual Vector2 GetOriginalSize()
        {
            return _size;
        }


        public virtual float GetRight()
        {
            return _position.X + _size.X;
        }


        public virtual float GetRotation()
        {
            return _rotation;
        }


        public virtual Vector2 GetSize()
        {
            return _size * _scale;
        }


        public virtual Color GetTint()
        {
            return _tint;
        }


        public virtual float GetTop()
        {
            return _position.Y;
        }


        public virtual Vector2 GetVelocity()
        {
            return _velocity;
        }


        public virtual float GetWidth()
        {
            return _size.X;
        }


        public virtual void Move()
        {
            if (_enabled)
            {
                _position = _position + _velocity;
            }
        }


        public virtual void Move(float gravity)
        {
            if (_enabled)
            {
                Vector2 force = new Vector2(_velocity.X, _velocity.Y + gravity);
                _position = _position + force;
            }
        }


        public virtual void MoveTo(Vector2 position)
        {
            _position = position;
        }


        public virtual void MoveTo(float x, float y)
        {
            _position = new Vector2(x, y);
        }


        public virtual bool Overlaps(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop());
        }


        public virtual void SizeTo(Vector2 size) 
        {
            _size = size;
        }


        public virtual void SizeTo(float width, float height) 
        {
            _size = new Vector2(width, height);
        }


        public virtual void Steer(float vx, float vy)
        {
            _velocity = new Vector2(vx, vy);
        }


        public virtual void Tint(Color color)
        {
            Validator.CheckNotNull(color);
            _tint = color;
        }

    }
}