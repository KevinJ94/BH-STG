using System;
using System.Collections.Generic;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace BH_STG.Sprites
{
    public class Invisible1: Enemy
    {
        private float _totaltimer;

        public Invisible1(Texture2D texture, ContentManager content, Vector2 pos, Vector2 direction)
            : base(texture)
        {
            this.isInvisible = true;
            BulletManager = new BulletManager(content);
            this.Position = pos;
            this.Direction = direction;
            this.ShootingTimer = 0.01f;
            this.Health = 9999;
            this._track = new CircleTrack();
            UpdateTrack(_track.Init(this));
        }

        public override void Shoot(List<Sprite> sprites)
        {
            var bullet =
                BulletManager.GetBullet(new Vector2(0, 1), Position, 1.5f, this, (int)Config.TrackStyle.TailTrack, 1, (int)Config.BulletStyle.Bullet1);
            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _totaltimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= ShootingTimer && _totaltimer < 3f)
            {
                Shoot(sprites);
                _timer = 0;
            }
            foreach (var sprite in sprites)
                if (sprite is Player)
                    UpdateTrack(_track.Move(this, sprite, gameTime));
            if (_totaltimer > 1f)
                this.IsRemoved = true;
        }


    }
}
