using System;
using frog.Things;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public class VillaScreen : IStage
    {
        public delegate VillaScreen Factory();

        private Vector2 _playerPosition;
        private float _frogSpeed;
        private Texture2D _backgroundTexture;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameState _gameState;

        public VillaScreen(SpriteBatch spriteBatch, ContentManager contentManager, GameState gameState, GraphicsDeviceManager graphics)
        {
            _spriteBatch = spriteBatch;
            _gameState = gameState;
            _graphics = graphics;

            _playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            _frogSpeed = 100f;

            _backgroundTexture = contentManager.Load<Texture2D>("mainBackground");
        }

        public void Draw()
        {
            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_gameState.Player.SmallSprite, _playerPosition, null, Color.White, 0f,
                new Vector2(_gameState.Player.SmallSprite.Width / 2, _gameState.Player.SmallSprite.Height / 2),
                new Vector2(2, 2),
                SpriteEffects.None,
                0f);
        }

        public void UpdateClick(MouseState mouseState)
        {
        }

        public void UpdateHover(MouseState mouseState)
        {
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
                _playerPosition.Y -= _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down))
                _playerPosition.Y += _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left))
                _playerPosition.X -= _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _playerPosition.X += _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_playerPosition.X > _graphics.PreferredBackBufferWidth - _gameState.Player.SmallSprite.Width / 2)
            {
                _playerPosition.X = _graphics.PreferredBackBufferWidth - _gameState.Player.SmallSprite.Width / 2;
            }
            else if (_playerPosition.X < _gameState.Player.SmallSprite.Width / 2)
            {
                _playerPosition.X = _gameState.Player.SmallSprite.Width / 2;
            }

            if (_playerPosition.Y > _graphics.PreferredBackBufferHeight - _gameState.Player.SmallSprite.Height / 2)
            {
                _playerPosition.Y = _graphics.PreferredBackBufferHeight - _gameState.Player.SmallSprite.Height / 2;
            }
            else if (_playerPosition.Y < _gameState.Player.SmallSprite.Height / 2)
            {
                _playerPosition.Y = _gameState.Player.SmallSprite.Height / 2;
            }
        }
    }
}
