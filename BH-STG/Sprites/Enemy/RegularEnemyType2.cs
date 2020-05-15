using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH_STG.Sprites
{
    public class RegularEnemyType2 : Enemy
    {
        private float _timer;
        private readonly Track _track = new SharpLTrack();
        private float bias;
        public RegularEnemyType2(Texture2D texture, Vector2 position)
            : base(texture)
        {
            Position = position;
            UpdateTrack(_track.Init(this));
            ShootingTimer = 1f;
            this.bias = 0.1f;
        }

        public override void Shoot(List<Sprite> sprites)
        {
            var bullet =
                BulletManager.GetBullet(new Vector2(0, 1), Position, 2, this, (int)Config.TrackStyle.TailTrack,1, (int)Config.BulletStyle.Bullet1);
            float b2x = bullet.Direction.X + bias;
            float b2y = (float)Math.Sqrt(1 - Math.Pow(b2x, 2));
            float b3x = bullet.Direction.X - bias;
            float b3y = (float)Math.Sqrt(1 - Math.Pow(b3x, 2));
            var b2 = new Vector2(b2x, b2y);
            var b3 = new Vector2(b3x, b3y);
            var bullet2 =
                BulletManager.GetBullet(b2, Position, 2, this, (int)Config.TrackStyle.NormalTrack,1, (int)Config.BulletStyle.Bullet1);
            var bullet3 =
               BulletManager.GetBullet(b3, Position, 2, this, (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet1);

            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootingTimer)
            {
                Shoot(sprites);
                _timer = 0;
            }
            foreach (var sprite in sprites)
                if (sprite is Player)
                    UpdateTrack(_track.Move(this, sprite, gameTime));
            // if the enemy is off the left side of the screen
            if (getScreenPos())
            {
                if (Position.X < -_texture.Width || Position.X > Game1.ScreenWidth + _texture.Width)
                    IsRemoved = true;
                if (Position.Y < -_texture.Height || Position.Y > Game1.ScreenHeight + _texture.Height)
                    IsRemoved = true;
            }
        }



    }
}
