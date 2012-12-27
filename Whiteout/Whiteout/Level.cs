
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
namespace Whiteout
{
    class Level
    {
        //Snowballs
        //Snowball Pile
        //Snow Mounds
        //Enemies

        public bool LevelComplete = false;
        private Player _player;
        private List<Snowball> _snowballs;
        private ContentManager _content;
        private Texture2D _levelBackground;

        public Level(ContentManager content, Player player)
        {
            _content = content;
            _player = player;
            _player.Position = new Vector2(500, 500);
            _snowballs = new List<Snowball>();
            _oldKeyboardState = Keyboard.GetState();
            _levelBackground = _content.Load<Texture2D>("LevelBackground");
        }

        public void Update(GameTime gameTime)
        {
            ManageInput();
            UpdateSnowballs(gameTime);
            _player.Update(gameTime);
        }

        private void UpdateSnowballs(GameTime gameTime)
        {
            foreach (Snowball snowball in _snowballs)
                snowball.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_levelBackground, Vector2.Zero, Color.White);
            _player.Draw(spriteBatch, gameTime);
            foreach (Snowball snowball in _snowballs)
                snowball.Draw(spriteBatch, gameTime);
        }

        public Player GetPlayer()
        {
            return _player;
        }

        private KeyboardState _oldKeyboardState;
        private KeyboardState _keyboardState;
        public void ManageInput()
        {
            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.A))
                _player.MoveLeft();
            else if (_keyboardState.IsKeyDown(Keys.D))
                _player.MoveRight();
            else
                _player.Accel.X = 0;

            if (_keyboardState.IsKeyDown(Keys.W))
                _player.MoveUp();
            else if (_keyboardState.IsKeyDown(Keys.S))
                _player.MoveDown();
            else
                _player.Accel.Y = 0;

            if (_keyboardState.IsKeyDown(Keys.LeftShift) || _keyboardState.IsKeyDown(Keys.RightShift))
                _player.Duck();
            else
                _player.StandUp();

            if (_keyboardState.IsKeyDown(Keys.Space))
                _player.ChargeThrow();

            if (_keyboardState.IsKeyUp(Keys.Space) && _oldKeyboardState.IsKeyDown(Keys.Space))
                LaunchSnowball();

            _oldKeyboardState = _keyboardState;
        }


        internal void LaunchSnowball()
        {
            bool aCreateNew = true;
            foreach (Snowball snowball in _snowballs)
            {
                if (snowball.Alive == false)
                {
                    aCreateNew = false;
                    snowball.Launch(new Vector2(_player.Position.X + _player.Width, _player.Position.Y), _player.ThrowCharge);
                    _player.ReleaseSnowball();
                    break;
                }
            }

            if (aCreateNew == true)
            {
                Snowball aSnowball = new Snowball(_content);
                aSnowball.Launch(new Vector2(_player.Position.X + _player.Width, _player.Position.Y), _player.ThrowCharge);
                _snowballs.Add(aSnowball);
            }

            _player.ReleaseSnowball();
        }
    }
}
