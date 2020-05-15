using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Utils
{
    public class ScrollingBackground: Sprite
    {

        public Vector2 Position;
        public Vector2 PositionUP;
        public float Layer { get; set; }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }
        public ScrollingBackground(Texture2D texture) : base(texture)
        {
            _texture = texture;

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            spriteBatch.Draw(_texture, Position, null, colour, 0f, Origin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_texture, PositionUP, null, colour, 0f, Origin, 1f, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Console.Out.WriteLine(Position.Y + " up" + PositionUP.Y);
            Position.Y++;
            PositionUP.Y++;
            if (PositionUP.Y == Game1.ScreenHeight / 2)
            {
                Position.Y = Game1.ScreenHeight / 2;
                PositionUP.Y = -Game1.ScreenHeight / 2;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Console.Out.WriteLine(Position.Y + " up" + PositionUP.Y);
            Position.Y ++;
            PositionUP.Y++;
            if (PositionUP.Y == Game1.ScreenHeight/2)
            {
                Position.Y=Game1.ScreenHeight/2;
                PositionUP.Y=-Game1.ScreenHeight/2;
            }
            
        }
    }
}
