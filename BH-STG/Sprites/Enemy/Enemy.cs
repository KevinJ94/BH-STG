using System;
using System.Collections.Generic;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Sprites
{
     public class Enemy : Ship
    {
        public float _timer;
        public float ShootingTimer;
        public float CrazyShootingTimer;
        public  Track _track;

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Enemy(Texture2D texture)
            : base(texture)
        {
            
        }

 
        public void UpdateTrack(Sprite sprite)
        {
            _rotation = sprite._rotation;
            Position = sprite.Position;
            IsRemoved = sprite.IsRemoved;
            Direction = sprite.Direction;
        }

        public override Rectangle Rectangle =>
            new Rectangle((int)Position.X-(int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);


        public override void OnCollide(Sprite sprite)
        {
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health -= sprite.Power;
                if (Health <= 0)
                {
                    sprite.Parent.Score++;
                    IsRemoved = true;
                }
                
            }

            if (sprite is Player)
            {
                if (sprite.InvincibleTime>0)
                {
                    return;
                }
                Health = 0;
                if (Health <= 0)
                {
                    //sprite.Parent.Score++;
                    IsRemoved = true;
                }
            }

        }
    }
}