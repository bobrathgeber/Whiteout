using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Whiteout
{
    class Snowball : GameObject
    {
        private const float AIR_DRAG = 0.99f;

        private float _fallTimeDuration = 1.0f;
        private float airTime;
        public bool AboveSnowMounds;
        public int Damage;
        public Player Owner;
        public bool PassedOverMound;

        public Snowball(ContentManager content)
        {
            Texture = content.Load<Texture2D>("snowball");
        }

        public override void Update(GameTime gameTime)
        {
            if (Alive)
            {
                airTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (airTime > _fallTimeDuration)
                    Alive = false;
                Velocity *= AIR_DRAG;
                base.Update(gameTime);
            }
        }

        public void Launch(Vector2 position, Vector2 velocity, bool aboveSnowMounds, Player owner)
        {
            PassedOverMound = false;
            Owner = owner;
            AboveSnowMounds = aboveSnowMounds;
            airTime = 0;
            Alive = true;
            Position = position;
            Velocity = velocity;
            Damage = 1;
        }
    }
}
