﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Models;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        public MenuState(Game1 game, ContentManager content) : base(game, content)
        {
            var t = new WaveHelper(content);
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");
            //var bg = _content.Load<Texture2D>("img_bg_menu");
            _components = new List<Component>()
            {
                new Sprite(_content.Load<Texture2D>("img_bg_menu"))
                {
                    Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2),
                },
                // new ScrollingBackground(_content.Load<Texture2D>("img_bg_menu"))
                // {
                //     Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2),
                //     PositionUP = new Vector2(Game1.ScreenWidth / 2, -Game1.ScreenHeight / 2)
                // },
                new Button(buttonTexture, buttonFont)
                {
                    Text = "Start",
                    Position = new Vector2(Game1.ScreenWidth / 2, 640),
                    Click = new EventHandler(Button_Start_Clicked),
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

        private void Button_Start_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content));
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
