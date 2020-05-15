using System.Collections.Generic;
using BH_STG.Sprites;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Manager
{
    public class BulletManager
    {
        private readonly List<Texture2D> _textures;


        public BulletManager(ContentManager content)
        {
            _textures = new List<Texture2D>
            {
                content.Load<Texture2D>("img_bullet"),
                content.Load<Texture2D>("img_bullet_1"),
                content.Load<Texture2D>("mid_boss_bullet"),
                content.Load<Texture2D>("final_boss_bullet")
            };
        }

        public Bullet GetBullet(Vector2 direction, Vector2 position, float linearVelocity, Sprite parent,
            int trackStyle,int power, int bulletStyle)
        {
            var bullet = new Bullet(_textures[bulletStyle], trackStyle)
            {
                Direction = direction,
                Position = position,
                LinearVelocity = linearVelocity * 2,
                LifeSpan = 2f,
                Parent = parent,
                Power = power
            };
            bullet.UpdateTrack(bullet._track.Init(bullet));
            return bullet;
        }

        public Bullet GetTrackBullet(Vector2 direction, Vector2 position, float linearVelocity, Sprite parent,
           int trackStyle, int power, int bulletStyle, Sprite target)
        {
            var bullet = new Bullet(_textures[bulletStyle], trackStyle)
            {
                Direction = direction,
                Position = position,
                LinearVelocity = linearVelocity * 2,
                LifeSpan = 2f,
                Parent = parent,
                Power = power
                
            };
            bullet.UpdateTrack(bullet._track.Init(bullet));
            bullet._track = new ConstTailTrack(target);
            return bullet;
        }
    }
}