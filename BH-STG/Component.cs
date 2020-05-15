using System.Collections.Generic;
using BH_STG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public abstract void Update(GameTime gameTime);
    }
}
