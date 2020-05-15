using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Interface;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Sprites.Boss
{

    public class MidBoss : Enemy
    {
        private static MidBoss Instance = new MidBoss(Game1.GlobalContent.Load<Texture2D>("img_plane_mid_boss"), Game1.GlobalContent);
        private float time;
        private ContentManager _content;
        private MidBoss(Texture2D texture, ContentManager content) : base(texture)
        {
            _content = content;
            BulletManager = new BulletManager(_content);
            _track = new Midboss_Track();
            LinearVelocity = 2f;
            RotationVelocity = 2f;
            UpdateTrack(_track.Init(this));
            ShootingTimer = 0.3f;
            CrazyShootingTimer = 3f;
            Position = new Vector2(256, 0);
            Direction = new Vector2(1, 0);
            Health = 100;
        }

        public static MidBoss GetInstance()
        {
            return Instance;
        }

        public static void Reset()
        {
            Instance = new MidBoss(Game1.GlobalContent.Load<Texture2D>("img_plane_mid_boss"), Game1.GlobalContent);
        }

        public void CrazyShoot(List<Sprite> sprites)
        {
            var bullet1 = new Invisible1(this._texture, _content, this.Position, new Vector2(0, 0));

            var bullet2 = new Invisible1(this._texture, _content, this.Position, new Vector2(3.14f, 0));

            //var bullet = BulletManager.GetBullet(new Vector2(0, 0), new Vector2(Position.X, Position.Y + 30), 3, this,
            //    (int) Config.TrackStyle.CircleTrack, 1, (int)Config.BulletStyle.Bullet2);

            //var bullet2 = BulletManager.GetBullet(new Vector2(3.14f, 0), new Vector2(Position.X, Position.Y + 30), 3, this,
            //    (int)Config.TrackStyle.CircleTrack, 1, (int)Config.BulletStyle.Bullet2);

            //var bullet3 = BulletManager.GetBullet(new Vector2(-1, 1), new Vector2(Position.X, Position.Y + 30), 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);

            //var bullet4 = BulletManager.GetBullet(new Vector2(0.5f, 1), new Vector2(Position.X, Position.Y + 30), 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);

            //var bullet5 = BulletManager.GetBullet(new Vector2(-0.5f, 1), new Vector2(Position.X, Position.Y + 30), 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);

            //if (Power == 0)
            //{
            //    Power = bullet.Power;
            //}
            sprites.Add(bullet1);
            sprites.Add(bullet2);
            //sprites.Add(bullet3);
            //sprites.Add(bullet4);
            //sprites.Add(bullet5);
        }

        public void LastShoot(List<Sprite> sprites)
        {
            var bullet = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet2);

            var bullet2 = BulletManager.GetBullet(new Vector2(-1, 1), new Vector2(Position.X - 40, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet2);

            var bullet3 = BulletManager.GetBullet(new Vector2(1, 1), new Vector2(Position.X + 40, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet2);


            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
        }

        public void NormalShoot(List<Sprite> sprites)
        {
            var bullet = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);

            var bullet2 = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X - 40, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);

            var bullet3 = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X + 40, Position.Y + 20), 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet2);


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
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time < 4f && time > 0)
            {
                if (_timer >= ShootingTimer)
                {
                    NormalShoot(sprites);
                    _timer = 0;
                }
            }
            if (time >= 4f && time < 12f)
            {
                if (_timer >= CrazyShootingTimer)
                {
                    CrazyShoot(sprites);
                    _timer = 0;
                }
            }
            if (time >= 13f)
            {
                if (_timer >= ShootingTimer)
                {
                    LastShoot(sprites);
                    _timer = 0;
                }
            }

            //if (_timer >= ShootingTimer)
            //    {
            //        Shoot1(sprites);
            //        _timer = 0;
            //    }


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
