using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Models;
using BH_STG.Sprites.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.States
{
    public class GameOver : State
    {

        private List<Component> _components;
        private int _code;
        public GameOver(Game1 game, ContentManager content,int code) : base(game, content)
        {
            _code = code;
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");
            _components = new List<Component>()
            {
                new Sprite(_content.Load<Texture2D>("over_"+_code))
                {
                    Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2),
                },
                new Button(buttonTexture, buttonFont)
                {
                    Text = "Back",
                    Position = new Vector2(Game1.ScreenWidth / 2, 640),
                    Click = new EventHandler(Button_Back_Clicked),
                    Layer = 0.1f
                },
                // new Button(buttonTexture, buttonFont)
                // {
                //     Text = "Highscores",
                //     Position = new Vector2(Game1.ScreenWidth / 2, 480),
                //     Click = new EventHandler(Button_Highscores_Clicked),
                //     Layer = 0.1f
                // },
                // new Button(buttonTexture, buttonFont)
                // {
                //     Text = "Quit",
                //     Position = new Vector2(Game1.ScreenWidth / 2, 520),
                //     Click = new EventHandler(Button_Quit_Clicked),
                //     Layer = 0.1f
                // },
            };
        }

        private void Button_Back_Clicked(object sender, EventArgs args)
        {
            MidBoss.Reset();
            FinalBoss.Reset();
            _game.ChangeState(new MenuState(_game, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
