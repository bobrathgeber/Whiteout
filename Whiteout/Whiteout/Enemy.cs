using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Whiteout
{
    class Enemy : Player
    {
        private float _attackTimer;
        public float AttackDelay = 1f;
        private float _attackCooldown = 0.5f;
        private int _accuracyDelta=200;
        private Level _level;

        public Enemy(ContentManager content, Vector2 position, Level level)
            : base(content)
        {
            _level = level;
            Position = position;
            ChargeRate = -0.2f;
            Alive = true;
            //_content = content;
            //Texture = content.Load<Texture2D>("Player");
            //Dictionary<string, Rectangle> spriteMap = content.Load<Dictionary<string, Rectangle>>("PlayerSpriteMap");
            //animation = new Animation(Texture, spriteMap);
            //animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
        }

        public override void Update(GameTime gameTime)
        {
            if (Alive)
            {
                _attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!_charging && _attackTimer > _attackCooldown)
                {
                    _charging = true;
                    _attackTimer = 0;
                    animationPlayer.PlayAnimation(animation, "PlayerForwardAttack", 0.3f, true);
                }
                else if (_charging && _attackTimer > AttackDelay)
                {
                    _level.LaunchSnowball(this);
                    animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
                    _attackTimer = 0;
                    ReleaseSnowball();
                }
                else if (_charging)
                    ChargeThrow();
            }

            base.Update(gameTime);
        }

        public override Vector2 GetThrowVelocity()
        {
            float delta = _level.Rand.Next(_accuracyDelta) - (_accuracyDelta/2);
            Console.WriteLine(delta);

            Vector2 throwDirection = _level.GetPlayer().GetCenter() - Position - new Vector2(Width + delta, 0);
            throwDirection.Normalize();
            return throwDirection * -ThrowCharge;
        }
    }
}
