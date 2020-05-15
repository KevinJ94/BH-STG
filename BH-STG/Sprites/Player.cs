using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using BH_STG.Models;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BH_STG.Sprites
{
    public class Player : Ship
    {
        public Input input;

        private readonly float _slowSpeed = 1f;
        private readonly float _shootIntervalTime = 0.1f;
        protected float timer = 0;
        private bool _canShoot = true;
        public int Lifetime = 3;
        
        protected List<Texture2D>  _textures;
        private List<Sprite> mySprites;

        public bool IsDead
        {
            get
            {
                return Lifetime <= 0 && Health <=0;
            }
        }


        // public Player(Texture2D texture)
        //     : base(texture)
        // {
        //     _slowSpeed = (float) 0.5 * LinearVelocity;
        //     Health = 1;
        //     InvincibleTime = 30;
        // }

        public Player(List<Texture2D> textures)
            : base(textures[0])
        {
            _textures = textures;
            _slowSpeed = (float)0.5 * LinearVelocity;
            Health = 1;
            InvincibleTime = 33222;
        }

        public override void Shoot(List<Sprite> sprites)
        {
            // _previousKey = _currentKey;
            // _currentKey = Keyboard.GetState();
            // if (_currentKey.IsKeyDown(Keys.Space) &&
            //     _previousKey.IsKeyUp(Keys.Space))
            //     AddBullet(sprites);

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space))
            {
                AddBullet(sprites);
                _canShoot = false;
            }
        }

        public override void Move()
        {
            var kstate = Keyboard.GetState();
            //normal mode
            if (kstate.IsKeyDown(input.Up))
                if (kstate.IsKeyDown(input.LeftShift))
                    Position.Y -= _slowSpeed;
                else
                    Position.Y -= LinearVelocity;

            if (kstate.IsKeyDown(input.Down))
                if (kstate.IsKeyDown(input.LeftShift))
                    Position.Y += _slowSpeed;
                else
                    Position.Y += LinearVelocity;

            if (kstate.IsKeyDown(input.Left))
                if (kstate.IsKeyDown(input.LeftShift))
                    Position.X -= _slowSpeed;
                else
                    Position.X -= LinearVelocity;

            if (kstate.IsKeyDown(input.Right))
                if (kstate.IsKeyDown(input.LeftShift))
                    Position.X += _slowSpeed;
                else
                    Position.X += LinearVelocity;
        }

        private void CheckBorder()
        {
            if (Position.X <= _texture.Width / 2) Position.X = _texture.Width / 2;

            if (Position.X >= Game1.ScreenWidth - _texture.Width / 2)
                Position.X = Game1.ScreenWidth - _texture.Width / 2;

            if (Position.Y <= _texture.Height / 2) Position.Y = _texture.Height / 2;

            if (Position.Y >= Game1.ScreenHeight - _texture.Height / 2)
                Position.Y = Game1.ScreenHeight - _texture.Height / 2;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            mySprites = sprites;
            if (InvincibleTime <= 0)
            {
                _texture = _textures[0];
            }
            else
            {
                _texture = _textures[1];
                InvincibleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            Move();
            CheckBorder();
            if (timer >= _shootIntervalTime)
            {
                timer = 0;
                _canShoot = true;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            if (_canShoot)
            {
                Shoot(sprites);
            }
            
        }


        public override void AddBullet(List<Sprite> sprites)
        {
            var bullet =
                BulletManager.GetBullet(Direction, Position, LinearVelocity, this, (int)Config.TrackStyle.NormalTrack,2, (int)Config.BulletStyle.Bullet0);
            if (Power == 0)
            {
                Power = bullet.Power;
            }
            sprites.Add(bullet);
        }

        public override Rectangle Rectangle =>
            new Rectangle(
                (int)Position.X - (int)Origin.X + (_texture.Width - _texture.Width / 4)/2, 
                (int)Position.Y - (int)Origin.Y + (_texture.Height - _texture.Height / 4) / 2, 
                _texture.Width / 4, 
                _texture.Height / 4);

        private void ClearBullet(List<Sprite> sprites)
        {
            
            foreach (var sprite in sprites)
            {
                if (sprite is Bullet)
                {
                    sprite.IsRemoved = true;
                }
            }
        }

        public override void OnCollide(Sprite sprite)
        {
            if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
            {
                if (InvincibleTime >0 )
                {
                    return;
                }
                Health -= sprite.Power;
                if (Health <= 0 && Lifetime<= 0)
                {
                    IsRemoved = true;
                }
                if (Health <= 0 && Lifetime > 0)
                {
                    Health = 1;
                    InvincibleTime = 3;
                    Lifetime--;
                    ClearBullet(mySprites);
                }
                // Console.Out.WriteLine(Health+"/"+Lifetime);
            }
            if (sprite is Enemy)
            {
                if (InvincibleTime > 0)
                {
                    return;
                }
                Health -= 1;
                if (Health <= 0 && Lifetime <= 0)
                {
                    IsRemoved = true;
                }
                if (Health <= 0 && Lifetime > 0)
                {
                    InvincibleTime = 3;
                    Health = 1;
                    Lifetime--;
                    ClearBullet(mySprites);
                }
            }
        }
    }
}