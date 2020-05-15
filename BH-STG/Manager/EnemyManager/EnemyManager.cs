using System.Collections.Generic;
using BH_STG.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BH_STG.Manager
{
    public class EnemyManager
    {
        private float _timer;

        public Texture2D _textures;
        
        public float SpawnTimer { get; set; }

        public ContentManager content;


        public EnemyManager(ContentManager content)
        {
            this.content = content;
        }

        public virtual Enemy GetEnemy(Vector2 direction, Vector2 position)
        {
            return null;
        }
    }
}