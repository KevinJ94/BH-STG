using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Interface;
using BH_STG.Manager;
using BH_STG.Models;
using BH_STG.Sprites;
using BH_STG.Sprites.Boss;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BH_STG.States
{
    public class GameState : State
    {
        private List<Sprite> _sprites;
        private Texture2D _bg;
        private List<Texture2D> _bgs;
        private Player player;
        private ScrollingBackground _background;
        private WaveHelper waveHelper;
        private SpriteFont _font;
        protected  double _stimer = 0;

        protected  double _mtimer = 0;

        protected  double _mtemp = 0;

        protected  int _min = 100;

        // private RegularEnemyManagerType1 _enemyManager1;
        // private RegularEnemyManagerType2 _enemyManager2;
        public GameState(Game1 game, ContentManager content) : base(game, content)
        {
            waveHelper = new WaveHelper(content);
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");
            
            _bg = _content.Load<Texture2D>("img_bg_level_2");
            List<Texture2D> playerTexture2Ds = new List<Texture2D>
            {
                _content.Load<Texture2D>("img_plane_main"),
                _content.Load<Texture2D>("img_plane_main_invincible"),
            };
            
            
            
            _sprites = new List<Sprite>
            {
                new ScrollingBackground(_content.Load<Texture2D>("img_bg_level_2"))
                {
                    Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2),
                    PositionUP = new Vector2(Game1.ScreenWidth / 2, -Game1.ScreenHeight / 2)
                },
                new Player(playerTexture2Ds)
                {
                    Position = new Vector2(256, 700),
                    Direction = new Vector2(0, -1),
                    input = new Input
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right,
                        LeftShift = Keys.LeftShift
                    },
                    BulletManager = new BulletManager(_content)
                }
            };

            player = (Player)_sprites.Where(c => c is Player).Single();
            _background = (ScrollingBackground)_sprites.Where(c => c is ScrollingBackground).Single();
        }

        public override void Update(GameTime gameTime)
        {
            //_bg = waveHelper.ChangeBg(gameTime);


            //This is a local state timer, nothing to do with  gameTime!!!
            //min unit: 100 Millisecond(See property _min in State.cs)
            //invoke GetWave for each second
            #region StateTimer

            _mtemp += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ((int)_mtimer / _min != (int)_mtemp / _min )
            {
                _mtimer = _mtemp;

                if ((int)_mtimer % 1000 == 0 && (int)_mtimer !=0 )
                {
                    _stimer++;
                    _mtimer = 0;
                    _mtemp = 0;
                }

                _background.SetTexture(waveHelper.ChangeBg(gameTime, (int) _stimer, (int) _mtimer));
                waveHelper.GetWave(_sprites, gameTime, (int)_stimer, (int)_mtimer);
                if (!waveHelper.CheckBossTime(gameTime))
                {
                    _game.ChangeState(new GameOver(_game,_content,1));
                }
                if (FinalBoss.GetInstance().IsDead)
                {
                    _game.ChangeState(new GameOver(_game, _content, 2));
                }

                // Console.Out.Write(((int)_stimer).ToString());
                // Console.Out.Write("  ");
                // Console.Out.WriteLine(((int)_mtimer).ToString());
            }

            #endregion


            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime,_sprites);
            
            PostUpdate(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

            var collidableSprites = _sprites.Where(c => c is ICollidable);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;
                    if (spriteA.Rectangle.Intersects(spriteB.Rectangle))
                    {
                        ((ICollidable)spriteA).OnCollide(spriteB);
                    }
                    // if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                    //     continue;
                    //
                    // if (spriteA.Intersects(spriteB))
                    //     ((ICollidable)spriteA).OnCollide(spriteB);
                }
            }

            for (var i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }

            if (player.IsDead)
            {
                _game.ChangeState(new GameOver(_game,_content,0));

            }

                
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(_bg, new Vector2(0, 0), Color.White);
            foreach (var sprite in _sprites)
            {
                if (!sprite.isInvisible)
                    sprite.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "Score: " + player.Score, new Vector2(10f, 10f), Color.White);
            spriteBatch.DrawString(_font, "Life Left: " + player.Lifetime, new Vector2(10f, 30f), Color.White);
            spriteBatch.End();
        }
    }
}
