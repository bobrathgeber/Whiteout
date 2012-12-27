using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiteout
{
    abstract class GameObject
    {
        //Physics Variables
        public Vector2 Velocity;
        public Vector2 Accel;
        public Vector2 Position;

        //Status Variables
        public bool Alive;
        protected int Health;

        //Visual Variables
        public Texture2D Texture { get; set; }
        protected AnimationPlayer animationPlayer;
        protected Animation animation;
        public Rectangle SrcRectangle { get { if (animation != null) return animation.GetFrameRectangle(animationPlayer.currentFrame); else return Texture.Bounds; } }

        public float Width { get { return SrcRectangle.Width; } }
        public float Height { get { return SrcRectangle.Height; } }
        public Rectangle GetBounds() { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); }
        public Rectangle GetHitBox() { return new Rectangle((int)Position.X, (int)(Position.Y * 0.75f), (int)Width, (int)(Height / 2)); }

        public virtual void Update(GameTime gameTime) 
        {
            Velocity += Accel * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity;        
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (animation != null)
                animationPlayer.Draw(gameTime, spriteBatch, Position, SpriteEffects.None, 0, Vector2.One, Vector2.Zero, Color.White);
            else if (Texture != null)
                spriteBatch.Draw(Texture, Position, SrcRectangle, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(SrcRectangle.Width/ 2 , SrcRectangle.Height/2) + Position;
        }

    }
}
