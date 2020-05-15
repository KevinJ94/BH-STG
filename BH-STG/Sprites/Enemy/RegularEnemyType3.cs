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
    public class RegularEnemyType3: Enemy
    {
        private float _timer;
        private float _totaltimer;
        public new bool IsGroupBullet = false;
        private readonly Track _track = new QueueLine();

        public RegularEnemyType3(Texture2D texture)
            : base(texture)
        {
            LinearVelocity = 4f;
            RotationVelocity = 4f;
            UpdateTrack(_track.Init(this));
            ShootingTimer = 2f;
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
            if (_totaltimer > 5f)
                this.IsRemoved = true;
        }


    }
}
