using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.States;
using Microsoft.Xna.Framework;

namespace BH_STG.Utils
{
    public class GentleLTrack : Track
    {
        private bool leftToRight;
        public override Sprite Init(Sprite sprite)
        {
            sprite.LinearVelocity = 4f;
            sprite._rotation = 1.57f;
            sprite.RotationVelocity = 2f;
            if (sprite.Position.X < 256)
            {
                this.leftToRight = true;
            }
            else
            {
                this.leftToRight = false;
            }
            return sprite;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            if (sprite.Position.Y < 192)
            {
                sprite.Position.Y += sprite.LinearVelocity;
            }
            else if (sprite.Position.Y >= 192 && sprite.Direction.Y > 0)
            {
                if (this.leftToRight == true)
                {
                    sprite._rotation -= MathHelper.ToRadians(sprite.RotationVelocity);
                }
                else {
                    sprite._rotation += MathHelper.ToRadians(sprite.RotationVelocity);
                }
                sprite.Direction = new Vector2((float)Math.Cos(sprite._rotation), (float)Math.Sin(sprite._rotation));
                sprite.Position += sprite.Direction * sprite.LinearVelocity;
            }
            else if (sprite.Direction.Y <= 0)
            {
                if (this.leftToRight == true)
                {
                    sprite.Position.X += sprite.LinearVelocity;
                }
                else
                {
                    sprite.Position.X -= sprite.LinearVelocity;
                }
            }
            return sprite;
        }
    }

    public class SharpLTrack : Track
    {
        private float _timer = 0;
        private bool leftToRight;
        private bool changeDirection;
        public override Sprite Init(Sprite sprite)
        {
            this.changeDirection = false;
            sprite.LinearVelocity = 4f;
            if (sprite.Position.X < 256)
            {
                this.leftToRight = true;
            }
            else
            {
                this.leftToRight = false;
            }
            return sprite;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            if (sprite.Position.Y < 192)
            {
                sprite.Position.Y += sprite.LinearVelocity;
            }
            else
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer < 1.5f)
                {
                    return sprite;
                }
                if (this.changeDirection == false) {
                    sprite.LinearVelocity = 2f;
                    this.changeDirection = true;
                    if (this.leftToRight == true)
                    {
                        sprite.Direction = new Vector2(1, 0);
                    }
                    else
                    {
                        sprite.Direction = new Vector2(-1, 0);
                    }
                }
                sprite.Position += sprite.Direction * sprite.LinearVelocity;
            }
            return sprite;
        }
    }

    public class QueueLine : Track
    {
        private float _timer = 0;
        public override Sprite Init(Sprite sprite)
        {
            sprite.LinearVelocity = 4f;
            if (sprite.Position.X < 256)
            {
                sprite.Direction = new Vector2(1, 0);
            }
            else
            {
                sprite.Direction = new Vector2(-1, 0);
            }
            return sprite;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 1.6f) {
                sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
                sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            }
            else if(_timer >2.6f){
                sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
                sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            }
            return sprite;  
        }
    }

    public class Horizontal : Track
    {
        private float _timer = 0;
        private float duration;
        private Vector2 Direction;
        public override Sprite Init(Sprite sprite)
        {
            this.duration = 2f;
            this.Direction = new Vector2(1, 0);
            sprite.LinearVelocity = 2f;
            return sprite;
        }

        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            if (sprite.Position.X == 480 && this.Direction == new Vector2(1, 0)) {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer < duration)
                {
                    return sprite;
                }
                else {
                    this.Direction = new Vector2(-1, 0);
                    _timer = 0;
                }
            }
            if (sprite.Position.X == 32 && this.Direction == new Vector2(-1, 0))
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer < duration)
                {
                    return sprite;
                }
                else
                {
                    this.Direction = new Vector2(1, 0);
                    _timer = 0;
                }
            }
            else
            {
                sprite.Position += this.Direction * sprite.LinearVelocity;
            }
            return sprite;
        }
    }

    public class Midboss_Track: Track
    {
        private float _timer = 0;
        //private float duration = 0;
        bool flag = true;

        public override Sprite Init(Sprite sprite)
        {
            sprite.LinearVelocity = 4f;
            return sprite;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer < 3.0f)
            {
                sprite.Position.Y += 1.6f;
            }

            if (_timer > 6.0f && _timer < 19.5f)
            {
                if (sprite.Position.X > 10 && flag == true)
                {
                    sprite.Position.X -= 3;
                    if (sprite.Position.X < 130)
                    {
                        flag = false;
                    }
                }
                if (sprite.Position.X < 450 && flag == false)
                {
                    sprite.Position.X += 3;
                    if (sprite.Position.X > 399)
                    {
                        flag = true;
                    }
                }
            }

            if (_timer > 21f && _timer < 23.5f)
            {
                sprite.Position.Y -= 1;
            }

            if (_timer > 25f && _timer < 27f)
            {
                sprite.Position.X -= 1;
            }

            if (_timer > 27f && _timer < 30f)
            {
                sprite.Position.Y -= 1;
            }

            return sprite;
        }

    }
}
