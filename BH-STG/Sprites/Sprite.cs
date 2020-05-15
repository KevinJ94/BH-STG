using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.States
{
    public  class Sprite: Component
    {
        protected Texture2D _texture;

        public float _rotation;

        public Vector2 Position;

        public Vector2 Origin;

        public int Score = 0;

        public Vector2 Direction;

        public int Power = 0;

        public float RotationVelocity = 3f;

        public float LinearVelocity = 4f;

        public Sprite Parent;

        public List<Sprite> ChildrenList = new List<Sprite>();

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        private bool InScreen;

        public float InvincibleTime = 0;

        public bool isInvisible = false;


        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public Texture2D getTexture()
        {
            return _texture;
        }

        public void SetTexture(Texture2D texture2D)
        {
            _texture = texture2D;
        }

        public bool getScreenPos()
        {
            return InScreen;
        }

        public virtual Rectangle Rectangle =>
            new Rectangle((int) Position.X-(int)Origin.X, (int) Position.Y-(int)Origin.Y, _texture.Width, _texture.Height);


        private void CheckEnter()
        {
            if (Position.X > 0 && Position.X < Game1.ScreenWidth - _texture.Width && Position.Y > 0 &&
                Position.Y < Game1.ScreenHeight - _texture.Height) InScreen = true;
        }

        // public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        // {
        //     CheckEnter();
        // }

        // public virtual void Draw(SpriteBatch spriteBatch)
        // {
        //     spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        // }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            CheckEnter();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}