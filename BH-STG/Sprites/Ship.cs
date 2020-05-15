using System.Collections.Generic;
using BH_STG.Interface;
using BH_STG.Manager;
using BH_STG.Models;
using BH_STG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BH_STG.Sprites
{
    public class Ship : Sprite, ICollidable
    {
        public BulletManager BulletManager;
        public int Health { get; set; }

        public Ship(Texture2D texture)
            : base(texture)
        {
        }


        public virtual void Shoot(List<Sprite> sprites){}

        public virtual void Move(){}

        public virtual void AddBullet(List<Sprite> sprites){}
        public virtual void OnCollide(Sprite sprite)
        {
            throw new System.NotImplementedException();
        }
    }
}