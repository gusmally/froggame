using System;
using System.Collections.Generic;
using frog.Screens.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public class OutsideScreen : IStage
    {
        public delegate OutsideScreen Factory();

        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private VillaScreen.Factory _villaScreenFactory;
        private SpriteFont _font;

        private Texture2D _outsideTexture;

        private Button _nextButton;

        private List<string> _introText = new List<string>
        {
            "Welcome to Frog Island!"
        };

        public OutsideScreen(GameState gameState, SpriteBatch spriteBatch, ContentManager contentManager, VillaScreen.Factory villaScreenFactory, SpriteFont font)
        {
            _gameState = gameState;
            _villaScreenFactory = villaScreenFactory;
            _spriteBatch = spriteBatch;
            _contentManager = contentManager;
            _font = font;

            _outsideTexture = _contentManager.Load<Texture2D>("outsideScreen");
            var nextArrowTexture = _contentManager.Load<Texture2D>("smallNextArrow");

            _nextButton = new Button(60, 435, nextArrowTexture, "", new Vector2(0,0));
        }

        public void Draw()
        {
            _spriteBatch.Draw(_outsideTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_nextButton.Texture, new Vector2(700, 510), Color.AliceBlue);

            _spriteBatch.DrawString(_font,
                _introText[0],
                new Vector2(60, 435),
                Color.White,
                0,
                new Vector2(0,0),
                0.5f,
                SpriteEffects.None,
                0.5f);
        }

        public void UpdateClick(MouseState mouseState)
        {

        }

        public void UpdateHover(MouseState mouseState)
        {
            
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {

        }
    }
}
