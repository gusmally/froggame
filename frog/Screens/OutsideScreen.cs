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
            "Welcome to Frog Island!",
            "lalalala",
            "text number 2",
            "ooga booga",
            "atatatat"


        };

        private int _introTextPointer = 0;

        public OutsideScreen(GameState gameState, SpriteBatch spriteBatch, ContentManager contentManager, VillaScreen.Factory villaScreenFactory, SpriteFont font)
        {
            _gameState = gameState;
            _villaScreenFactory = villaScreenFactory;
            _spriteBatch = spriteBatch;
            _contentManager = contentManager;
            _font = font;

            _outsideTexture = _contentManager.Load<Texture2D>("outsideScreen");
            var nextArrowTexture = _contentManager.Load<Texture2D>("smallNextArrow");

            _nextButton = new Button(700, 510, nextArrowTexture, "", new Vector2(0,0));
        }

        public void Draw()
        {
            _spriteBatch.Draw(_outsideTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_nextButton.Texture,
                              new Vector2(_nextButton.Viewport.X, _nextButton.Viewport.Y),
                              Color.AliceBlue);

            _spriteBatch.DrawString(_font,
                _introText[_introTextPointer],
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
            _nextButton.SetHasBeenClicked(mouseState);

            if (_nextButton.HasBeenClicked)
            {
                if (_introTextPointer < _introText.Count - 1)
                {
                    _introTextPointer++;
                    _nextButton.HasBeenClicked = false;
                }
                else
                {
                    // move to the next view or whatever
                    // maybe create an object to hold events that happen, a "slide"?
                        // - animation
                        // - accompanying text
                        // - whether that advance button can be pressed or not (to skip)
                }
            }
        }

        public void UpdateHover(MouseState mouseState)
        {
            _nextButton.SetHoverState(mouseState);
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {

        }
    }
}
