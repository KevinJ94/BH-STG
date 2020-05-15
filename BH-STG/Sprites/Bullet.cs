using System;
using System.Collections.Generic;
using BH_STG.Interface;
using BH_STG.Manager;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Sprites
{
    public class Bullet : Sprite, ICollidable
    {
        private float _timer;
        public Track _track;

        public Bullet(Texture2D texture, int trackStyle)
            : base(texture)
        {
            _track = TrackManager.GetTrack(trackStyle);
            UpdateTrack(_track.Init(this));
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
                if (sprite is Player)
                    UpdateTrack(_track.Move(this, sprite, gameTime)); 

            if (Position.X < 0 ||
                Position.X > Game1.ScreenWidth + getTexture().Width)
                IsRemoved = true;
            if (Position.Y < 0 ||
                Position.Y > Game1.ScreenHeight + getTexture().Height)
                IsRemoved = true;
        }

        public void UpdateTrack(Sprite sprite)
        {
            _rotation = sprite._rotation;
            Position = sprite.Position;
            IsRemoved = sprite.IsRemoved;
            Direction = sprite.Direction;
        }

        public override Rectangle Rectangle =>
            new Rectangle(
                (int)Position.X - (int)Origin.X + (_texture.Width - _texture.Width / 2) / 2,
                (int)Position.Y - (int)Origin.Y + (_texture.Height - _texture.Height / 2) / 2,
                _texture.Width / 2,
                _texture.Height / 2);

        public void OnCollide(Sprite sprite)
        {
            if (sprite is Bullet)
                return;

            // Enemies can't shoot eachother
            if (sprite is Enemy && this.Parent is Enemy)
                return;

            // Players can't shoot eachother
            if (sprite is Player && this.Parent is Player)
                return;

            // Can't hit a player if they're dead
            // if (sprite is Player && ((Player)sprite).IsDead)
            //     return;

            if (sprite is Enemy && this.Parent is Player)
            {
                IsRemoved = true;
            }

            if (sprite is Player && this.Parent is Enemy)
            {
                IsRemoved = true;
            }
        }
    }
}