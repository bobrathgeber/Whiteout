using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Whiteout
{
    class Obstacle : GameObject
    {
        public Obstacle(ContentManager content, Vector2 position)
        {
            Texture = content.Load<Texture2D>("SnowMound");
            Position = position;
        }        
    }
}
