using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Sprites.Boss
{
    public class FinalBoss : Enemy
    {
        public List<Texture2D> textures;
        private static FinalBoss Instance = new FinalBoss(new List<Texture2D> {
            Game1.GlobalContent.Load<Texture2D>("img_plane_final_boss"),
            Game1.GlobalContent.Load<Texture2D>("img_plane_crazy_final_boss")
        }
        , Game1.GlobalContent);
        
        private float time;
        private ContentManager _content;
        public FinalBoss(List<Texture2D> _textures, ContentManager content)
            : base(_textures[0])
        {
            _content = content;
            BulletManager = new BulletManager(content);
            _track = new Midboss_Track();
            LinearVelocity = 2f;
            RotationVelocity = 2f;
            UpdateTrack(_track.Init(this));
            ShootingTimer = 0.3f;
            CrazyShootingTimer = 3f;
            Position = new Vector2(256, 0);
            Direction = new Vector2(1, 0);
            Health = 100;
            textures = new List<Texture2D>();
            textures = _textures;
        }

        //private FinalBoss(Texture2D texture, ContentManager content) : base(texture)
        //{
        //    _content = content;
        //    BulletManager = new BulletManager(content);
        //    _track = new Midboss_Track();
        //    LinearVelocity = 2f;
        //    RotationVelocity = 2f;
        //    UpdateTrack(_track.Init(this));
        //    ShootingTimer = 0.3f;
        //    CrazyShootingTimer = 3f;
        //    Position = new Vector2(256, 0);
        //    Direction = new Vector2(1, 0);
        //    Health = 100;
        //}

        public static FinalBoss GetInstance()
        {
            return Instance;
        }

        public static void Reset()
        {
            Instance = new FinalBoss(new List<Texture2D> {
            Game1.GlobalContent.Load<Texture2D>("img_plane_final_boss"),
            Game1.GlobalContent.Load<Texture2D>("img_plane_crazy_final_boss")
        }
        , Game1.GlobalContent);
        }

        public void NormalShoot(List<Sprite> sprites)
        {
            var bullet =
                BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X - 50, Position.Y + 40), 3, this,
                (int)Config.TrackStyle.NormalWithBias, 1, (int)Config.BulletStyle.Bullet3);
            var bullet1 =
                BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X - 20, Position.Y + 40), 3, this,
                (int)Config.TrackStyle.NormalWithBias, 1, (int)Config.BulletStyle.Bullet3);
            var bullet2 =
                BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X + 20, Position.Y + 40), 3, this,
                (int)Config.TrackStyle.NormalWithBias, 1, (int)Config.BulletStyle.Bullet3);
            var bullet3 =
                BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X + 50, Position.Y + 40), 3, this,
                (int)Config.TrackStyle.NormalWithBias, 1, (int)Config.BulletStyle.Bullet3);


            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
            sprites.Add(bullet1);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
        }

        public void CrazyShoot(List<Sprite> sprites)
        {
            var player = (Player)sprites.Where(c => c is Player).Single();
            double scale = 1000;
            double dist1 = player.Position.X - this.Position.X;
            double dist2 = player.Position.Y - this.Position.Y;
            double angle = Math.Atan2(dist2, dist1);

            var left = new Sprite(_texture) { isInvisible = true };
            var right = new Sprite(_texture) { isInvisible = true };

            double leftAngle = angle + (2 * Math.PI / 3);
            double leftX = this.Position.X + scale * (Math.Cos(leftAngle));
            double leftY = this.Position.Y + scale * (Math.Sin(leftAngle));
            double rightAngle = angle - (2 * Math.PI / 3);
            double rightX = this.Position.X + scale * (Math.Cos(rightAngle));
            double rightY = this.Position.Y + scale * (Math.Sin(rightAngle));

            left.Position = new Vector2((float)leftX, (float)leftY);
            right.Position = new Vector2((float)rightX, (float)rightY);

            var bullet = new Invisible2(this._texture, _content, this.Position, new Vector2(0, 0));
            bullet.left = left;
            bullet.right = right;
            sprites.Add(bullet);
            sprites.Add(left);
            sprites.Add(right);
        }


        public void S_CrazyShoot(List<Sprite> sprites)
        {
            //var bullet =
            //    BulletManager.GetBullet(new Vector2(0, 1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet1 =
            //    BulletManager.GetBullet(new Vector2(1, 0), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet2 =
            //    BulletManager.GetBullet(new Vector2(-1, 1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet3 =
            //    BulletManager.GetBullet(new Vector2(1, -1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet4 =
            //    BulletManager.GetBullet(new Vector2(0, -1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet5 =
            //    BulletManager.GetBullet(new Vector2(-1, 0), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet6 =
            //    BulletManager.GetBullet(new Vector2(1, 1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            //var bullet7 =
            //    BulletManager.GetBullet(new Vector2(-1, -1), Position, 3, this,
            //    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet =
                 BulletManager.GetBullet(new Vector2(0, 1), Position, 3, this,
                 (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet1 =
                BulletManager.GetBullet(new Vector2(1, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet2 =
                BulletManager.GetBullet(new Vector2(-1, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet3 =
                BulletManager.GetBullet(new Vector2(0.25f, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet4 =
             BulletManager.GetBullet(new Vector2(-0.25f, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet5 =
    BulletManager.GetBullet(new Vector2(0.75f, 1), Position, 3, this,
    (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet6 =
             BulletManager.GetBullet(new Vector2(-0.75f, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet7 =
BulletManager.GetBullet(new Vector2(0.50f, 1), Position, 3, this,
(int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);
            var bullet8 =
             BulletManager.GetBullet(new Vector2(-0.50f, 1), Position, 3, this,
                (int)Config.TrackStyle.NormalTrack, 1, (int)Config.BulletStyle.Bullet3);



            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
            sprites.Add(bullet1);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
            sprites.Add(bullet4);
            sprites.Add(bullet5);
            sprites.Add(bullet6);
            sprites.Add(bullet7);
            sprites.Add(bullet8);
        }

        public void Trace_Shoot(List<Sprite> sprites) {
            var bullet = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X - 50, Position.Y + 40), 3, this,
                (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet2);
            var bullet1 = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X - 20, Position.Y + 40), 3, this,
        (int)Config.TrackStyle.NormalStayTrack, 1, (int)Config.BulletStyle.Bullet2);
            var bullet2 = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X + 20, Position.Y + 40), 3, this,
(int)Config.TrackStyle.NormalStayTrack, 1, (int)Config.BulletStyle.Bullet2);
            var bullet3 = BulletManager.GetBullet(new Vector2(0, 1), new Vector2(Position.X + 50, Position.Y + 40), 3, this,
    (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet2);


            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
            sprites.Add(bullet1);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
        
    }





        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time < 5f && time > 1f)
            {
                if (_timer >= ShootingTimer)
                {
                    Trace_Shoot(sprites);
                    _timer = 0;
                }
            }
            if (time >= 5f && time < 10f)
            {
                if (_timer >= ShootingTimer)
                {
                    Trace_Shoot(sprites);
                    _timer = 0;
                }
            }

            if (time >= 13f && time < 21.5f)
            {
                _texture = textures[1];

                if (_timer >= CrazyShootingTimer)
                {
                    CrazyShoot(sprites);
                    _timer = 0;
                }
            }
            if (time >= 22.5f)
            {
                if (_timer >= ShootingTimer)
                {
                    S_CrazyShoot(sprites);
                    _timer = 0;
                }
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
