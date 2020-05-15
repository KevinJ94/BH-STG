using BH_STG.Sprites;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH_STG.Manager
{
    public class RegularEnemyManagerType3 : EnemyManager
    {
        public RegularEnemyManagerType3(ContentManager content) : base(content)
        {
            _textures = content.Load<Texture2D>("img_plane_enemy_3");
            this.content = content;
        }

        public override Enemy GetEnemy(Vector2 direction, Vector2 position)
        {
            return new RegularEnemyType3(_textures)
            {
                Direction = direction,
                BulletManager = new BulletManager(content),
                Position = position,
                Health = 2
            };

        }
    }
}