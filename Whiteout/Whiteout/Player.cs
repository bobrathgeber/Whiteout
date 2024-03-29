﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Whiteout
{
    class Player : GameObject
    {
        protected const float FRICTION = 0.96f;
        //Stats
        public float Traction = 25;
        public float MaxMoveSpeed = 15;
        public float ThrowSpeed = 15;
        public float ChargeRate = 0.5f;
        public int Health;
        protected bool _ducking = false;
        protected bool _charging = false;
        public float ThrowCharge { get; set; }

        protected ContentManager _content;

        public Player(ContentManager content)
        {
            _content = content;
            Texture = content.Load<Texture2D>("Player");
            Dictionary<string, Rectangle> spriteMap = content.Load<Dictionary<string, Rectangle>>("PlayerSpriteMap");
            animation = new Animation(Texture, spriteMap);
            animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
            Health = 5;

        }

        public override void Update(GameTime gameTime)
        {
            Velocity *= FRICTION;
            Velocity.X = MathHelper.Clamp(Velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -MaxMoveSpeed, MaxMoveSpeed);
            base.Update(gameTime);
        }

        public void TakeDamage(Snowball snowball)
        {
            Health -= snowball.Damage;
            snowball.Alive = false;
            if (Health <= 0)
            {
                Alive = false;
                animationPlayer.PlayAnimation(animation, "PlayerForwardAttack", 0.3f, true);
            }
        }

        public void ChargeThrow()
        {
            _charging = true;
            ThrowCharge += ChargeRate;
            if (ThrowCharge > ThrowSpeed)
                ThrowCharge = ThrowSpeed;
            if(_ducking)
                animationPlayer.PlayAnimation(animation, "PlayerDuckAttack", 0.3f, true);
            else
                animationPlayer.PlayAnimation(animation, "PlayerForwardAttack", 0.3f, true);
        }

        public void ReleaseSnowball()
        {
            _charging = false;
            ThrowCharge = 0;
            if (_ducking)
                animationPlayer.PlayAnimation(animation, "PlayerDuck", 0.3f, true);
            else
                animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
        }

        public void MoveUp()
        {
            Accel.Y = -Traction;
            animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
        }

        public void MoveDown()
        {
            Accel.Y = Traction;
            animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
        }

        public void MoveLeft()
        {
            Accel.X = -Traction;
            animationPlayer.PlayAnimation(animation, "PlayerSide", 0.3f, true);
        }

        public void MoveRight()
        {
            Accel.X = Traction;
            animationPlayer.PlayAnimation(animation, "PlayerSide", 0.3f, true);
        }

        public void Duck()
        {
            _ducking = true;
            if(_charging)
                animationPlayer.PlayAnimation(animation, "PlayerDuckAttack", 0.3f, true);
            else
                animationPlayer.PlayAnimation(animation, "PlayerDuck", 0.3f, true);
        }

        public void StandUp()
        {
            _ducking = false;
            if (_charging)
                animationPlayer.PlayAnimation(animation, "PlayerForwardAttack", 0.3f, true);
            else
                animationPlayer.PlayAnimation(animation, "PlayerForward", 0.3f, true);
        }

        public bool IsDucking()
        {
            return _ducking;
        }

        public virtual Vector2 GetThrowVelocity()
        {
            return new Vector2(0, -ThrowCharge) + (Velocity / 2);
        }
    }
}
