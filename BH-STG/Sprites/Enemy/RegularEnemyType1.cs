using System.Collections.Generic;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Sprites
{
    public  class RegularEnemyType1 : Enemy
    {

        public RegularEnemyType1(Texture2D texture, Vector2 position) 
            :base(texture)
        {
            Position = position;
            _timer = 0f;
            _track = new GentleLTrack();
            UpdateTrack(_track.Init(this));
            ShootingTimer = 1f;
        }

        public override void Shoot(List<Sprite> sprites)
        {
            var bullet =
                BulletManager.GetBullet(new Vector2(0, 1), Position, 2.5f, this, (int)Config.TrackStyle.NormalWithBias,1, (int)Config.BulletStyle.Bullet1);
            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
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
