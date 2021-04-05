using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog
{
    public partial class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _playerPosition;
        private float _frogSpeed;
        private bool _leftButtonDepressed;
        private bool _rightButtonDepressed;
        private bool _readyButtonHovered;
        private MouseState _lastMouseState;
        private Pronoun _characterCreationSelectedPronoun;

        private List<Character> _characters = new List<Character>();
        private Character _player;

        //private Texture2D _frogTexture;
        private Texture2D _backgroundTexture;
        private Texture2D _titleTexture;
        private Texture2D _characterCreationTexture;

        private List<Texture2D> _characterTextureOptions = new List<Texture2D>();
        private List<Texture2D> _smallCharacterTextureOptions = new List<Texture2D>();
        private int _characterPointer;

        private Texture2D _leftArrow;
        private Texture2D _rightArrow;
        private Texture2D _nextButton;

        private Texture2D _sheHighlight;
        private Texture2D _heHighlight;
        private Texture2D _theyHighlight;
        private Texture2D _customHighlight;

        private SpriteFont _font;

        private State _state;
        private enum State { TitleScreen, CharacterCreation, MainGame };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            _playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            _frogSpeed = 100f;

            _state = State.TitleScreen;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture = this.Content.Load<Texture2D>("mainBackground");
            _titleTexture = this.Content.Load<Texture2D>("title");

            // character creation
            _characterCreationTexture = this.Content.Load<Texture2D>("characterCreation");

            _characterTextureOptions.Add(this.Content.Load<Texture2D>("big_option0"));
            _characterTextureOptions.Add(this.Content.Load<Texture2D>("big_option1"));
            _characterTextureOptions.Add(this.Content.Load<Texture2D>("big_option2"));
            _characterTextureOptions.Add(this.Content.Load<Texture2D>("big_option3"));
            _characterTextureOptions.Add(this.Content.Load<Texture2D>("big_option4"));

            _smallCharacterTextureOptions.Add(this.Content.Load<Texture2D>("option0"));
            _smallCharacterTextureOptions.Add(this.Content.Load<Texture2D>("option1"));
            _smallCharacterTextureOptions.Add(this.Content.Load<Texture2D>("option2"));
            _smallCharacterTextureOptions.Add(this.Content.Load<Texture2D>("option3"));
            _smallCharacterTextureOptions.Add(this.Content.Load<Texture2D>("option4"));

            _rightArrow = this.Content.Load<Texture2D>("rightArrow");
            _leftArrow = this.Content.Load<Texture2D>("leftArrow");
            _nextButton = this.Content.Load<Texture2D>("nextArrow");

            _sheHighlight = this.Content.Load<Texture2D>("sheHighlight");
            _heHighlight = this.Content.Load<Texture2D>("heHighlight");
            _theyHighlight = this.Content.Load<Texture2D>("theyHighlight");
            _customHighlight = this.Content.Load<Texture2D>("customHighlight");

            // font
            _font = this.Content.Load<SpriteFont>("dearLovely");

            // characters
            _characters.Add(new Character("Ash", Pronoun.They, this.Content.Load<Texture2D>("ash"), this.Content.Load<Texture2D>("big_ash")));
            _characters.Add(new Character("Bigstick", Pronoun.They, this.Content.Load<Texture2D>("bigstick"), this.Content.Load<Texture2D>("big_bigstick")));
            _characters.Add(new Character("Cubi", Pronoun.They, this.Content.Load<Texture2D>("cubi"), this.Content.Load<Texture2D>("big_cubi")));
            _characters.Add(new Character("Juicebox", Pronoun.They, this.Content.Load<Texture2D>("juicebox"), this.Content.Load<Texture2D>("big_juicebox")));
            _characters.Add(new Character("Kate", Pronoun.They, this.Content.Load<Texture2D>("kate"), this.Content.Load<Texture2D>("big_kate")));
            _characters.Add(new Character("Lastone", Pronoun.They, this.Content.Load<Texture2D>("lastone"), this.Content.Load<Texture2D>("big_lastone")));
            _characters.Add(new Character("Mike", Pronoun.They, this.Content.Load<Texture2D>("mike"), this.Content.Load<Texture2D>("big_mike")));
            _characters.Add(new Character("Omar", Pronoun.They, this.Content.Load<Texture2D>("omar"), this.Content.Load<Texture2D>("big_omar")));
            _characters.Add(new Character("Pea", Pronoun.They, this.Content.Load<Texture2D>("pea"), this.Content.Load<Texture2D>("big_pea")));
            _characters.Add(new Character("Shim", Pronoun.They, this.Content.Load<Texture2D>("shim"), this.Content.Load<Texture2D>("big_shim")));
            _characters.Add(new Character("Tode", Pronoun.They, this.Content.Load<Texture2D>("tode"), this.Content.Load<Texture2D>("big_tode")));
            _characters.Add(new Character("Willow", Pronoun.They, this.Content.Load<Texture2D>("willow"), this.Content.Load<Texture2D>("big_willow")));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            /*
            MouseState currentState = Mouse.GetState(); 
            if (currentState.LeftButton == ButtonState.Pressed &&
                _lastMouseState.LeftButton == ButtonState.Released) 
            {
                display = !display; 
            }
            */

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            switch (_state)
            {
                case State.TitleScreen:
                    this.titleScreenUpdate(mouseState);
                    break;
                case State.CharacterCreation:
                    this.characterCreationScreenUpdate(mouseState);
                    break;
                case State.MainGame:
                    this.moveFrog(keyboardState, gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (_state)
            {
                case State.TitleScreen:
                    _spriteBatch.Draw(_titleTexture, new Vector2(0, 0), Color.AliceBlue);
                    break;

                case State.CharacterCreation:
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
                        case Pronoun.Custom:
                            _spriteBatch.Draw(_customHighlight, new Vector2(0, -132), Color.AliceBlue);
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

                    break;

                case State.MainGame:
                    _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.AliceBlue);
                    _spriteBatch.Draw(_player.SmallSprite, _playerPosition, null, Color.White, 0f,
                        new Vector2(_player.SmallSprite.Width / 2, _player.SmallSprite.Height / 2),
                        new Vector2(2, 2),
                        SpriteEffects.None,
                        0f);
                    break;
            }

            /*
            string text = "hello hellooooo poopoo";
            // Finds the center of the string in coordinates inside the text rectangle
            Vector2 textMiddlePoint = _font.MeasureString(text) / 2;
            // Places text in center of the screen
            Vector2 position = new Vector2(this.Window.ClientBounds.Width / 2,
                this.Window.ClientBounds.Height / 2);
            _spriteBatch.DrawString(_font, text, position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            */
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void titleScreenUpdate(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mouseState.Y > 348 && mouseState.Y < 458)
                {
                    if (mouseState.X > 72 && mouseState.X < 319)
                    {
                        _state = State.CharacterCreation;
                    }
                }
            }
        }

        private void characterCreationScreenUpdate(MouseState mouseState)
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

            // mouse state
            if (mouseState == _lastMouseState)
                return;

            _lastMouseState = mouseState;

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
                // custom button
                if (mouseState.Y > 373 && mouseState.Y < 453)
                {
                    if (mouseState.X > 586 && mouseState.X < 800)
                    {
                        _characterCreationSelectedPronoun = Pronoun.Custom;
                    }
                }

                // ready button
                if (mouseState.Y > 503 && mouseState.Y < 572)
                {
                    if (mouseState.X > 521 && mouseState.X < 779)
                    {
                        _player = new Character("test", Pronoun.She, _smallCharacterTextureOptions[_characterPointer], _characterTextureOptions[_characterPointer]);
                        _state = State.MainGame;
                    }
                }
            }
            else
            {
                _rightButtonDepressed = false;
                _leftButtonDepressed = false;
            }
        }

        private void moveFrog(KeyboardState keyboardState, GameTime gameTime)
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

            if (_playerPosition.X > _graphics.PreferredBackBufferWidth - _player.SmallSprite.Width / 2)
            {
                _playerPosition.X = _graphics.PreferredBackBufferWidth - _player.SmallSprite.Width / 2;
            }
            else if (_playerPosition.X < _player.SmallSprite.Width / 2)
            {
                _playerPosition.X = _player.SmallSprite.Width / 2;
            }

            if (_playerPosition.Y > _graphics.PreferredBackBufferHeight - _player.SmallSprite.Height / 2)
            {
                _playerPosition.Y = _graphics.PreferredBackBufferHeight - _player.SmallSprite.Height / 2;
            }
            else if (_playerPosition.Y < _player.SmallSprite.Height / 2)
            {
                _playerPosition.Y = _player.SmallSprite.Height / 2;
            }
        }
    }
}
