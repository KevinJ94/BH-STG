using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.Manager;
using BH_STG.Models;
using BH_STG.Sprites.Boss;
using BH_STG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Utils
{
    public class WaveHelper
    {
        
        private RegularEnemyManagerType1 _enemyManager1;
        private RegularEnemyManagerType2 _enemyManager2;
        private RegularEnemyManagerType3 _enemyManager3;
        private bool IsMidBossShowed = false;
        private bool IsFinalBossShowed = false;
        private float _midTimer = 3;
        private float _finalTimer = 4;
        private Data _config;

        private ContentManager content;
        public WaveHelper(ContentManager _content)
        {
            _enemyManager1 = new RegularEnemyManagerType1(_content);
            _enemyManager2 = new RegularEnemyManagerType2(_content);
            _enemyManager3 = new RegularEnemyManagerType3(_content);
            content = _content;
            ReadConfigFile();
        }

        private void ReadConfigFile()
        {
            _config = JsonHelper.JsonFileToObj();
        }

        private void WriteConfigFile()
        {
            List<Wave> wList = new List<Wave>();
            Wave w = new Wave(0,1,0,0,0,0, "RegularEnemyType1");
            wList.Add(w);
            wList.Add(w);
            var data = new Data();
            data.data = wList;
            JsonHelper.ObjToJsonFile(data);
        }

        public void GetWave(List<Sprite>  _sprites, GameTime gameTime, int sTimer, int mTimer)
        {
            // First(_sprites, gameTime, sTimer, mTimer);
            // MidBoss(_sprites, gameTime, sTimer, mTimer);
            // SecondStage(_sprites, gameTime, sTimer, mTimer);
            // FinalBoss(_sprites, gameTime, sTimer, mTimer);
            foreach (var wave in _config.data)
            {
                
                if (sTimer == wave._sTimer && mTimer == wave._mTimer)
                {
                    if (wave._ClassName == "RegularEnemyType1")
                    {
                        _sprites.Add(
                            _enemyManager1.GetEnemy(
                                new Vector2(wave._dirX, wave._dirY),
                                new Vector2(wave._posX, wave._posY)
                            )
                        );
                        _config.data.Remove(wave);
                        break;
                    }
                    if (wave._ClassName == "RegularEnemyType2")
                    {
                        _sprites.Add(
                            _enemyManager2.GetEnemy(
                                new Vector2(wave._dirX, wave._dirY),
                                new Vector2(wave._posX, wave._posY)
                            )
                        );
                        _config.data.Remove(wave);
                        break;
                    }
                    if (wave._ClassName == "RegularEnemyType3")
                    {
                        _sprites.Add(
                            _enemyManager3.GetEnemy(
                                new Vector2(wave._dirX, wave._dirY),
                                new Vector2(wave._posX, wave._posY)
                            )
                        );
                        _config.data.Remove(wave);
                        break;

                    }
                    if (wave._ClassName == "MidBoss")
                    {
                        _sprites.Add(
                            Sprites.Boss.MidBoss.GetInstance()
                        );
                        IsMidBossShowed = true;
                        _config.data.Remove(wave);
                        break;
                    }
                    if (wave._ClassName == "FinalBoss")
                    {
                        //if (sTimer == 72 && mTimer == 0)
                        //{
                            _sprites.Add(
                                Sprites.Boss.FinalBoss.GetInstance()
                            );
                            IsFinalBossShowed = true;
                        _config.data.Remove(wave);
                            break;
                        //}
                    }
                }

                
            }

        }

        public bool CheckBossTime(GameTime gameTime)
        {

            if (IsMidBossShowed && !Sprites.Boss.MidBoss.GetInstance().IsDead)
            {
                //Console.Out.WriteLine("mid : "+_midTimer);
                _midTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_midTimer <0 && !Sprites.Boss.MidBoss.GetInstance().IsDead)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            if (IsFinalBossShowed && !Sprites.Boss.FinalBoss.GetInstance().IsDead)
            {
                //Console.Out.WriteLine("final :"+_finalTimer);
                _finalTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_finalTimer < 0 && !Sprites.Boss.FinalBoss.GetInstance().IsDead)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }

            return true;
        }


        public Texture2D ChangeBg(GameTime gameTime, int sTimer, int mTimer)
        {
            if (sTimer > 0 && sTimer <= 31)
            {
                return content.Load<Texture2D>("img_bg_level_2");
            }
            else if (sTimer >31 && sTimer <= 79)
            {
                return content.Load<Texture2D>("img_bg_level_3");
            }
            else if (sTimer > 79)
            {
                return content.Load<Texture2D>("img_bg_level_4");
            }
            else
            {
                return content.Load<Texture2D>("img_bg_level_2");
            }


        }

        public void First(List<Sprite> _sprites, GameTime gameTime, int sTimer, int mTimer) {
            //Wave 1
            if (sTimer == 1 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 1 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );

            ////Wave 2
            if (sTimer == 3 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 3 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );

            //Wave 3
            if (sTimer == 6 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager2.GetEnemy(new Vector2(0, 1), new Vector2(342, 0))
                );

            //Wave 4
            if (sTimer == 8 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 8 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 8 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );

            //Wave 5
            if (sTimer == 10 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 10 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 10 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );

            //Wave 6
            if (sTimer == 12 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager2.GetEnemy(new Vector2(0.7f, 1), new Vector2(170, 0))
                );

            //Wave7
            if (sTimer == 14 &&mTimer == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(1, 0), new Vector2(0 - 50 * i, 240))
                         );
                }
            }

            //Wave 8
            if (sTimer == 18 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 18 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 18 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );

            //Wave 9
            if (sTimer == 20 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 20 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 20 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );

            //Wave 10
            if (sTimer == 20 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 20 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 20 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );

            //Wave 11
            if (sTimer == 22 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                );
            if (sTimer == 22 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                );
            if (sTimer == 22 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                    );

            //Wave 12
            if (sTimer == 22 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                );
            if (sTimer == 22 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                );
            if (sTimer == 22 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                    );

            //Wave 13
            if (sTimer == 24 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                );
            if (sTimer == 24 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                );
            if (sTimer == 24 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                    );

            //Wave 14
            if (sTimer == 26 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                );
            if (sTimer == 26 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                );
            if (sTimer == 26 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                    );

            //Wave 15
            if (sTimer == 26 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                );
            if (sTimer == 26 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                );
            if (sTimer == 26 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                    );

            //Wave 16
            if (sTimer == 28 &&mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 28 &&mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 28 &&mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );

            //Wave 17
            if (sTimer == 30 &&mTimer == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(1, 0), new Vector2(0 - 50 * i, 240))
                         );
                }
            }
        }

        public void MidBoss(List<Sprite> _sprites, GameTime gameTime, int sTimer, int mTimer)
        {
            
            //Mid boss
            if (sTimer == 32 && mTimer == 0)
            {
                _sprites.Add(
                        Sprites.Boss.MidBoss.GetInstance()
                );
                IsMidBossShowed = true;
            }
                
        }

        public void SecondStage(List<Sprite> _sprites, GameTime gameTime, int sTimer, int mTimer) {
            //Wave 1
            if (sTimer == 41 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 41 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 41 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 41 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );

            ////Wave 2
            if (sTimer == 43 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 43 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 43 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 43 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );

            //Wave 3
            if (sTimer == 46 && mTimer == 0)
                _sprites.Add(
                    _enemyManager2.GetEnemy(new Vector2(0, 1), new Vector2(342, 0))
                );

            //Wave 4
            if (sTimer == 48 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 48 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 48 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 48 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );
            if (sTimer == 48 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );

            //Wave 5
            if (sTimer == 50 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 50 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 50 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 50 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );
            if (sTimer == 50 && mTimer == 800)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );

            //Wave 6
            if (sTimer == 52 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0.7f, 1), new Vector2(170, 0))
                );

            //Wave7
            if (sTimer == 54 && mTimer == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(1, 0), new Vector2(0 - 50 * i, 240))
                         );
                }
            }

            //Wave 8
            if (sTimer == 58 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 58 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 58 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                );
            if (sTimer == 58 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );
            if (sTimer == 58 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(448, 0))
                    );

            //Wave 9
            if (sTimer == 60 && mTimer == 0)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 60 && mTimer == 200)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 60 && mTimer == 400)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                );
            if (sTimer == 60 && mTimer == 600)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );
            if (sTimer == 60 && mTimer == 800)
                _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(0, 1), new Vector2(64, 0))
                    );

            //Wave 10
            if (sTimer == 60 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 60 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 60 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 60 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );
            if (sTimer == 60 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );

            //Wave 11
            if (sTimer == 62 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                );
            if (sTimer == 62 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                );
            if (sTimer == 62 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                );
            if (sTimer == 62 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                    );
            if (sTimer == 62 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(128, 0))
                    );

            //Wave 12
            if (sTimer == 62 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                );
            if (sTimer == 62 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                );
            if (sTimer == 62 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                );
            if (sTimer == 62 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                    );
            if (sTimer == 62 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(270, 0))
                    );

            //Wave 13
            if (sTimer == 64 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                );
            if (sTimer == 64 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                );
            if (sTimer == 64 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                );
            if (sTimer == 64 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                    );
            if (sTimer == 64 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(240, 0))
                    );

            //Wave 14
            if (sTimer == 66 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                );
            if (sTimer == 66 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                );
            if (sTimer == 66 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                );
            if (sTimer == 66 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                    );
            if (sTimer == 66 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(462, 0))
                    );

            //Wave 15
            if (sTimer == 66 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                );
            if (sTimer == 66 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                );
            if (sTimer == 66 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                );
            if (sTimer == 66 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                    );
            if (sTimer == 66 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(50, 0))
                    );

            //Wave 16
            if (sTimer == 68 && mTimer == 0)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 68 && mTimer == 200)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 68 && mTimer == 400)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                );
            if (sTimer == 68 && mTimer == 600)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );
            if (sTimer == 68 && mTimer == 800)
                _sprites.Add(
                    _enemyManager1.GetEnemy(new Vector2(0, 1), new Vector2(320, 0))
                    );

            //Wave 17
            if (sTimer == 70 && mTimer == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    _sprites.Add(
                    _enemyManager3.GetEnemy(new Vector2(1, 0), new Vector2(0 - 50 * i, 240))
                         );
                }
            }
        }

        public void FinalBoss(List<Sprite> _sprites, GameTime gameTime, int sTimer, int mTimer) {           
            //Final boss
            if (sTimer == 72 && mTimer == 0)
            {
                _sprites.Add(
                    Sprites.Boss.FinalBoss.GetInstance()
                );
                IsFinalBossShowed = true;
            }
                
        }
    }
}
