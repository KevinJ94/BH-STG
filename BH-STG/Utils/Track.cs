using System;
using BH_STG.States;
using Microsoft.Xna.Framework;

namespace BH_STG.Utils
{
    public abstract class Track
    {
        public virtual Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            return sprite;
        }


        public virtual Sprite Init(Sprite sprite)
        {
            return sprite;
        }
    }

    public class NormalTrack : Track
    {
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
            sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            return sprite;
        }
    }

    public class NormalWithBias : Track
    {
        private double scale;
        public override Sprite Init(Sprite sprite)
        {
            this.scale = 0.3;
            Random ran = new Random();
            double r = ran.NextDouble();
            double bais = r * this.scale;
            double isPositive = ran.NextDouble();
            if (isPositive > 0.5)
            {
                sprite.Direction.X += (float)bais;
                sprite.Direction.Y = (float)Math.Sqrt(1 - Math.Pow(sprite.Direction.X, 2));
            }
            else
            {
                sprite.Direction.X -= (float)bais;
                sprite.Direction.Y = (float)Math.Sqrt(1 - Math.Pow(sprite.Direction.X, 2));
            }
            return sprite;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
            sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            return sprite;
        }
    }

    public class CircleTrack : Track
    {
        public override Sprite Init(Sprite sprite)
        {
            sprite._rotation = sprite.Direction.X;
            sprite.LinearVelocity = 18f;
            sprite.RotationVelocity = 20f;
            return sprite;
        }

        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            sprite.RotationVelocity -= 0.25f;
            sprite._rotation -= MathHelper.ToRadians(sprite.RotationVelocity);
            sprite.Direction = new Vector2((float) Math.Cos(sprite._rotation), (float) Math.Sin(sprite._rotation));
            sprite.Position += sprite.Direction * sprite.LinearVelocity;
            return sprite;
        }
    }

    public class CircleTrack2 : Track
    {
        public override Sprite Init(Sprite sprite)
        {
            sprite._rotation = sprite.Direction.X;
            sprite.LinearVelocity = 23f;
            sprite.RotationVelocity = 22f;
            return sprite;
        }

        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            sprite.RotationVelocity -= 0.2f;
            sprite._rotation -= MathHelper.ToRadians(sprite.RotationVelocity);
            sprite.Direction = new Vector2((float)Math.Cos(sprite._rotation), (float)Math.Sin(sprite._rotation));
            sprite.Position += sprite.Direction * sprite.LinearVelocity;
            return sprite;
        }
    }

    public class NormalStayTrack : Track
    {
        private float _timer = 0;
        public override Sprite Move(Sprite sprite,Sprite target, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 2)
            {
                sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
                sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            }

            if (_timer > 3)
            {
                sprite.IsRemoved = true;
            }

            return sprite;
        }
    }

    public class TailTrack : Track
    {
        private float _timer = 0;
        public override Sprite Move(Sprite sprite,Sprite target, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer <0.02)
            {
                float d_x = target.Position.X - sprite.Position.X;
                float d_y = target.Position.Y - sprite.Position.Y;
                float distance = (float)Math.Sqrt(d_x* d_x + d_y* d_y);
                d_x = d_x / distance;
                d_y = d_y / distance;
                sprite.Direction = new Vector2(d_x, d_y);
            }
            sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
            sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            if (sprite.Position.X < 0 ||
                sprite.Position.X > Game1.ScreenWidth + sprite.getTexture().Width)
                sprite.IsRemoved = true;
            if (sprite.Position.Y < 0 ||
                sprite.Position.Y > Game1.ScreenHeight + sprite.getTexture().Height)
                sprite.IsRemoved = true;
            return sprite;
        }
    }

    public class ConstTailTrack : Track
    {
        private float _timer = 0;
        private Sprite target;
        public ConstTailTrack(Sprite target)
        {
            this.target = target;
        }
        public override Sprite Move(Sprite sprite, Sprite target, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 0.02)
            {
                float d_x = this.target.Position.X - sprite.Position.X;
                float d_y = this.target.Position.Y - sprite.Position.Y;
                float distance = (float)Math.Sqrt(d_x * d_x + d_y * d_y);
                d_x = d_x / distance;
                d_y = d_y / distance;
                sprite.Direction = new Vector2(d_x, d_y);
            }
            sprite.Position.X += sprite.Direction.X * sprite.LinearVelocity;
            sprite.Position.Y += sprite.Direction.Y * sprite.LinearVelocity;
            if (sprite.Position.X < 0 ||
                sprite.Position.X > Game1.ScreenWidth + sprite.getTexture().Width)
                sprite.IsRemoved = true;
            if (sprite.Position.Y < 0 ||
                sprite.Position.Y > Game1.ScreenHeight + sprite.getTexture().Height)
                sprite.IsRemoved = true;
            return sprite;
        }
    }

}