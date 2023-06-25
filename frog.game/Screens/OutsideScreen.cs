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

        private Vector2 _playerPosition;
        private float _frogSpeed;
        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private VillaScreen.Factory _villaScreenFactory;
        private SpriteFont _font;
        private GraphicsDeviceManager _graphics;

        private Texture2D _outsideTexture;

        private Button _nextButton;

        private List<string> _introText = new List<string>
        {
            "Welcome to Frog Island!",
            "We've been waiting for you",
            "In just a few short moments, your adventure to find love \r\n and the frog of your dreams will begin",
            "Are you ready?",
            "Enter the villa and begin the next amazing chapter of your life!"
        };

        private int _introTextPointer = 0;

        public OutsideScreen(GameState gameState, SpriteBatch spriteBatch, ContentManager contentManager, VillaScreen.Factory villaScreenFactory, SpriteFont font, GraphicsDeviceManager graphics)
        {
            _gameState = gameState;
            _villaScreenFactory = villaScreenFactory;
            _spriteBatch = spriteBatch;
            _contentManager = contentManager;
            _font = font;
            _graphics = graphics;

            _outsideTexture = _contentManager.Load<Texture2D>("outsideScreen");
            var nextArrowTexture = _contentManager.Load<Texture2D>("smallNextArrow");

            // this could be collected into a player class
            _playerPosition = new Vector2(430, 350);
            _frogSpeed = 100f;

            _nextButton = new Button(700, 510, nextArrowTexture, "", new Vector2(0,0));
        }

        public void Draw()
        {
            _spriteBatch.Draw(_outsideTexture, new Vector2(0, 0), Color.AliceBlue);
            _spriteBatch.Draw(_nextButton.Texture,
                              new Vector2(_nextButton.Viewport.X, _nextButton.Viewport.Y),
                              Color.AliceBlue);

            // todo: measure the string and draw it appropriately-- a helper class
            // would be good for this
            _spriteBatch.DrawString(_font,
                _introText[_introTextPointer],
                new Vector2(60, 435),
                Color.White,
                0,
                new Vector2(0,0),
                0.5f,
                SpriteEffects.None,
                0.5f);

            _spriteBatch.Draw(_gameState.Player.SmallSprite, _playerPosition, null, Color.White, 0f,
                new Vector2(_gameState.Player.SmallSprite.Width / 2, _gameState.Player.SmallSprite.Height / 2),
                new Vector2(2, 2),
                SpriteEffects.None,
                0f);

            // todo: not really part of draw. do we need a new method for check?
            if (_introTextPointer == _introText.Count -1 &&
                _playerPosition.X >= 680 &&
                _playerPosition.X <= 715 &&
                _playerPosition.Y >= 240 &&
                _playerPosition.Y <= 280)
            {
                // move to the next view or whatever
                // maybe create an object to hold events that happen, a "slide"?
                // - animation
                // - accompanying text
                // - whether that advance button can be pressed or not (to skip)
                _gameState.CurrentStage = _villaScreenFactory();
            }
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
            }
        }

        public void UpdateHover(MouseState mouseState)
        {
            _nextButton.SetHoverState(mouseState);
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
            // todo: coalesce player movement into a helper class
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
