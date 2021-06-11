using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public class TitleScreen : IStage
    {
        public delegate TitleScreen Factory();

        private GameState _gameState;
        private CharacterScreen.Factory _characterScreenFactory;
        private SpriteBatch _spriteBatch;
        private Texture2D _titleTexture;

        public TitleScreen(GameState gameState, SpriteBatch spriteBatch, ContentManager contentManager, CharacterScreen.Factory characterScreenFactory)
        {
            _gameState = gameState;
            _characterScreenFactory = characterScreenFactory;
            _spriteBatch = spriteBatch;

            _titleTexture = contentManager.Load<Texture2D>("title");
        }

        public void Draw()
        {
            _spriteBatch.Draw(_titleTexture, new Vector2(0, 0), Color.AliceBlue);
        }

        public void UpdateClick(MouseState mouseState)
        {
            if (mouseState.Y > 348 && mouseState.Y < 458)
            {
                if (mouseState.X > 72 && mouseState.X < 319)
                {
                    _gameState.CurrentStage = _characterScreenFactory();
                }
            }
        }

        public void UpdateHover(MouseState mouseState)
        {
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
        }
    }
}
