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

        private Texture2D _backgroundTexture;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameState _gameState;

        public VillaScreen(SpriteBatch spriteBatch, ContentManager contentManager, GameState gameState, GraphicsDeviceManager graphics)
        {
            _spriteBatch = spriteBatch;
            _gameState = gameState;
            _graphics = graphics;

            _gameState.Player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);

            _backgroundTexture = contentManager.Load<Texture2D>("mainBackground");
        }

        public void Draw()
        {
            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_gameState.Player.SmallSprite, _gameState.Player.Position, null, Color.White, 0f,
                new Vector2(_gameState.Player.SmallSprite.Width / 2, _gameState.Player.SmallSprite.Height / 2),
                new Vector2(2, 2),
                _gameState.Player.SpriteEffects,
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
            _gameState.Player.UpdateKeyboard(keyboardState, gameTime);
        }
    }
}
