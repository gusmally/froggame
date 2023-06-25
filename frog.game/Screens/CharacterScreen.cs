using System.Collections.Generic;
using frog.Things;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public class CharacterScreen : IStage
    {
        public delegate CharacterScreen Factory();

        private Texture2D _characterCreationTexture;
        private Pronoun _characterCreationSelectedPronoun;
        private SpriteBatch _spriteBatch;
        private int _characterPointer;
        private Texture2D _leftArrow;
        private Texture2D _rightArrow;
        private bool _leftButtonDepressed;
        private bool _rightButtonDepressed;
        private bool _readyButtonHovered;
        private Texture2D _sheHighlight;
        private Texture2D _heHighlight;
        private Texture2D _theyHighlight;
        private Texture2D _customHighlight;
        private GameState _gameState;
        private List<Texture2D> _characterTextureOptions = new List<Texture2D>();
        private List<Texture2D> _smallCharacterTextureOptions = new List<Texture2D>();
        private Texture2D _nextButton;
        private ContentManager _contentManager;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private OccupationScreen.Factory _occupationScreenFactory;

        public CharacterScreen(SpriteBatch spriteBatch, ContentManager contentManager, GameState gameState, OccupationScreen.Factory occupationScreenFactory, GraphicsDeviceManager graphicsDeviceManager)
        {
            _contentManager = contentManager;

            // character creation
            _characterCreationTexture = _contentManager.Load<Texture2D>("characterCreation");
            _rightArrow = _contentManager.Load<Texture2D>("rightArrow");
            _leftArrow = _contentManager.Load<Texture2D>("leftArrow");

            _characterTextureOptions.Add(_contentManager.Load<Texture2D>("big_option0"));
            _characterTextureOptions.Add(_contentManager.Load<Texture2D>("big_option1"));
            _characterTextureOptions.Add(_contentManager.Load<Texture2D>("big_option2"));
            _characterTextureOptions.Add(_contentManager.Load<Texture2D>("big_option3"));
            _characterTextureOptions.Add(_contentManager.Load<Texture2D>("big_option4"));

            _smallCharacterTextureOptions.Add(_contentManager.Load<Texture2D>("option0"));
            _smallCharacterTextureOptions.Add(_contentManager.Load<Texture2D>("option1"));
            _smallCharacterTextureOptions.Add(_contentManager.Load<Texture2D>("option2"));
            _smallCharacterTextureOptions.Add(_contentManager.Load<Texture2D>("option3"));
            _smallCharacterTextureOptions.Add(_contentManager.Load<Texture2D>("option4"));

            _nextButton = _contentManager.Load<Texture2D>("nextArrow");

            _sheHighlight = _contentManager.Load<Texture2D>("sheHighlight");
            _heHighlight = _contentManager.Load<Texture2D>("heHighlight");
            _theyHighlight = _contentManager.Load<Texture2D>("theyHighlight");
            _customHighlight = _contentManager.Load<Texture2D>("customHighlight");
            _spriteBatch = spriteBatch;
            _gameState = gameState;
            _occupationScreenFactory = occupationScreenFactory;
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Draw()
        {
            _spriteBatch.Draw(_characterCreationTexture, new Vector2(0, 0), Color.AliceBlue);

            // left/right buttons
            if (_leftButtonDepressed)
            {
                _spriteBatch.Draw(_leftArrow, new Vector2(0, 5), Color.AliceBlue);
            }
            else
            {
                _spriteBatch.Draw(_leftArrow, new Vector2(0, 0), Color.AliceBlue);
            }

            if (_rightButtonDepressed)
            {
                _spriteBatch.Draw(_rightArrow, new Vector2(0, 5), Color.AliceBlue);
            }
            else
            {
                _spriteBatch.Draw(_rightArrow, new Vector2(0, 0), Color.AliceBlue);
            }

            if (_leftButtonDepressed || _rightButtonDepressed)
            {
                _spriteBatch.Draw(_characterTextureOptions[_characterPointer], new Vector2(40, 148), Color.AliceBlue);
            }
            else
            {
                _spriteBatch.Draw(_characterTextureOptions[_characterPointer], new Vector2(40, 153), Color.AliceBlue);
            }

            // pronouns
            switch (_characterCreationSelectedPronoun)
            {
                case Pronoun.She:
                    _spriteBatch.Draw(_sheHighlight, new Vector2(0, -132), Color.AliceBlue);
                    break;
                case Pronoun.He:
                    _spriteBatch.Draw(_heHighlight, new Vector2(0, -132), Color.AliceBlue);
                    break;
                case Pronoun.They:
                    _spriteBatch.Draw(_theyHighlight, new Vector2(0, -132), Color.AliceBlue);
                    break;
            }

            // ready button
            if (_readyButtonHovered)
            {
                _spriteBatch.Draw(_nextButton, new Vector2(0, 5), Color.AliceBlue);
            }
            else
            {
                _spriteBatch.Draw(_nextButton, new Vector2(0, 0), Color.AliceBlue);
            }
        }

        public void UpdateClick(MouseState mouseState)
        {
            // skin buttons
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mouseState.Y > 433 && mouseState.Y < 517)
                {
                    // left button
                    if (mouseState.X > 78 && mouseState.X < 168)
                    {
                        _leftButtonDepressed = true;

                        if (_characterPointer == 4)
                        {
                            _characterPointer = 0;
                        }
                        else
                        {
                            _characterPointer++;
                        }
                    }

                    // right button
                    if (mouseState.X > 237 && mouseState.X < 331)
                    {
                        _rightButtonDepressed = true;

                        if (_characterPointer == 0)
                        {
                            _characterPointer = 4;
                        }
                        else
                        {
                            _characterPointer--;
                        }
                    }
                }

                // she button
                if (mouseState.Y > 221 && mouseState.Y < 301)
                {
                    if (mouseState.X > 426 && mouseState.X < 563)
                    {
                        _characterCreationSelectedPronoun = Pronoun.She;
                    }
                }
                // he button
                if (mouseState.Y > 343 && mouseState.Y < 435)
                {
                    if (mouseState.X > 415 && mouseState.X < 554)
                    {
                        _characterCreationSelectedPronoun = Pronoun.He;
                    }
                }
                // they button
                if (mouseState.Y > 201 && mouseState.Y < 327)
                {
                    if (mouseState.X > 602 && mouseState.X < 785)
                    {
                        _characterCreationSelectedPronoun = Pronoun.They;
                    }
                }

                // ready button
                if (mouseState.Y > 503 && mouseState.Y < 572)
                {
                    if (mouseState.X > 521 && mouseState.X < 779)
                    {
                        //_gameState.CurrentStage = GameStage.Occupation;
                        _gameState.Player = new Character("test", _characterCreationSelectedPronoun, _smallCharacterTextureOptions[_characterPointer], _characterTextureOptions[_characterPointer], _graphicsDeviceManager);
                        _gameState.CurrentStage = _occupationScreenFactory();
                    }
                }
            }
            else // TODO
            {
                _rightButtonDepressed = false;
                _leftButtonDepressed = false;
            }
        }

        public void UpdateHover(MouseState mouseState)
        {
            // ready button
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
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
        }
    }
}
