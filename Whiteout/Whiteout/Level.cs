
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
namespace Whiteout
{
    class Level
    {
        //Snowball Pile

        public bool LevelComplete = false;
        private Player _player;
        private List<Snowball> _snowballs;
        private List<Obstacle> _obstacles;
        private List<Enemy> _enemies;
        private ContentManager _content;
        private Texture2D _levelBackground;
        private Texture2D _lifeCounterImage;

        public Level(ContentManager content, Player player)
        {
            _content = content;
            _player = player;
            _player.Position = new Vector2(500, 500);
            _snowballs = new List<Snowball>();
            _obstacles = new List<Obstacle>();
            _enemies = new List<Enemy>();
            _oldKeyboardState = Keyboard.GetState();
            _levelBackground = _content.Load<Texture2D>("LevelBackground");
            _lifeCounterImage = _content.Load<Texture2D>("playerHat");
            AddGameObjects();
        }

        private void AddGameObjects()
        {
            _obstacles.Add(new Obstacle(_content, new Vector2(100, 30)));
            _obstacles.Add(new Obstacle(_content, new Vector2(700, 30)));
            _obstacles.Add(new Obstacle(_content, new Vector2(400, 450)));

            _enemies.Add(new Enemy(_content, new Vector2(400, 30), this));
            _enemies.Add(new Enemy(_content, new Vector2(0, 0), this));
        }

        public void Update(GameTime gameTime)
        {
            ManageInput();
            UpdateSnowballs(gameTime);
            foreach (Enemy enemy in _enemies)
                enemy.Update(gameTime);
            _player.Update(gameTime);
        }

        private void UpdateSnowballs(GameTime gameTime)
        {
            foreach (Snowball snowball in _snowballs)
            {
                snowball.Update(gameTime);

                foreach (Obstacle obstacle in _obstacles)
                {
                    if (snowball.Alive && CheckCollision(snowball, obstacle) && snowball.AboveSnowMounds)
                        snowball.Alive = false;
                }
                foreach (Enemy enemy in _enemies)
                {
                    if (snowball.Alive && CheckCollision(snowball, enemy) && snowball.Owner != enemy)
                        enemy.TakeDamage(snowball);
                }

                if (snowball.Alive && CheckCollision(snowball, _player) && snowball.Owner != _player)
                    _player.TakeDamage(snowball);
            }
        }

        private bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            return obj1.GetHitBox().Intersects(obj2.GetHitBox());
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_levelBackground, Vector2.Zero, Color.White);
            foreach (Obstacle obstacle in _obstacles)
                obstacle.Draw(spriteBatch, gameTime);
            foreach (Enemy enemy in _enemies)
                enemy.Draw(spriteBatch, gameTime);
            _player.Draw(spriteBatch, gameTime);
            foreach (Snowball snowball in _snowballs)
                snowball.Draw(spriteBatch, gameTime);
            DrawLives(spriteBatch);
            
        }

        private void DrawLives(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _player.Health; i++)
                spriteBatch.Draw(_lifeCounterImage, new Vector2(-70 * i - _lifeCounterImage.Width + 1280, 10), Color.White);
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

            if (_keyboardState.IsKeyDown(Keys.LeftShift))
                _player.Duck();
            else if(_oldKeyboardState.IsKeyDown(Keys.LeftShift) && _keyboardState.IsKeyUp(Keys.LeftShift))
                _player.StandUp();

            if (_keyboardState.IsKeyDown(Keys.Space))
                _player.ChargeThrow();

            if (_keyboardState.IsKeyUp(Keys.Space) && _oldKeyboardState.IsKeyDown(Keys.Space))
                LaunchSnowball(_player);

            _oldKeyboardState = _keyboardState;
        }


        internal void LaunchSnowball(Player thrower)
        {
            bool aCreateNew = true;
            foreach (Snowball snowball in _snowballs)
            {
                if (false)//snowball.Alive == false)
                {
                    aCreateNew = false;
                    snowball.Launch(new Vector2(thrower.Position.X + thrower.Width, thrower.Position.Y), thrower.GetThrowVelocity(), thrower.IsDucking(), thrower);
                    thrower.ReleaseSnowball();
                    break;
                }
            }

            if (aCreateNew == true)
            {
                Snowball aSnowball = new Snowball(_content);
                aSnowball.Launch(new Vector2(thrower.Position.X + thrower.Width, thrower.Position.Y), thrower.GetThrowVelocity(), thrower.IsDucking(), thrower);
                _snowballs.Add(aSnowball);
            }

            thrower.ReleaseSnowball();
        }
    }
}
