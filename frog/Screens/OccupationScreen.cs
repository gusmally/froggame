using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using frog;
using frog.Things;

namespace frog.Screens
{
    public class OccupationScreen : IStage
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Texture2D _occupationTexture;
        private Texture2D _occupation1;
        private Texture2D _nextButton;
        private MouseState _lastMouseState;

        private ContentManager _contentManager;
        private GameState _gameState;
        private Character _player;

        private bool _readyButtonHovered;

        public OccupationScreen(SpriteBatch spriteBatch, SpriteFont font, ContentManager contentManager, GameState gameState, Character player)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _contentManager = contentManager;
            _gameState = gameState;
            _player = player;

            _occupationTexture = _contentManager.Load<Texture2D>("occupations");
            _occupation1 = _contentManager.Load<Texture2D>("occupations(1)");
            _nextButton = _contentManager.Load<Texture2D>("nextArrow");
        }

        public void Draw()
        {
            _spriteBatch.Draw(_occupationTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_occupation1, new Vector2(0, 0), Color.AliceBlue);

            string nanny = "nanny";
            string barber = "barber";
            string electrician = "electrician";
            string ceramicist = "ceramicist";

            // Places text in center of the screen
            _spriteBatch.DrawString(_font, nanny, new Vector2(100, 150), Color.White, 0, _font.MeasureString(nanny) / 2, 1.0f, SpriteEffects.None, 0.5f);

            // ready button
            if (_readyButtonHovered)
            {
                _spriteBatch.Draw(_nextButton, new Vector2(0, 15), Color.AliceBlue);
            }
            else
            {
                _spriteBatch.Draw(_nextButton, new Vector2(0, 10), Color.AliceBlue);
            }
        }

        public void Update(MouseState mouseState)
        {
            if (mouseState.Y > 503 && mouseState.Y < 572)
            {
                if (mouseState.X > 521 && mouseState.X < 779)
                {
                    _readyButtonHovered = true;
                }
            }
            else
            {
                _readyButtonHovered = false;
            }

            // mouse state
            if (mouseState.LeftButton == _lastMouseState.LeftButton)
                return;

            _lastMouseState = mouseState;

            if (mouseState.LeftButton == ButtonState.Released)
                return;

            // handle the buttons being pressed
            //Occupation chosen = ;

            // ready button
            if (mouseState.Y > 503 && mouseState.Y < 572)
            {
                if (mouseState.X > 521 && mouseState.X < 779)
                {
                    _player.Occupation = new Occupation("test");
                    _gameState.Stage = GameState.GameStage.MainGame;
                }
            }
        }
    }
}
