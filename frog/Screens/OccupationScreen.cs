using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using frog;
using frog.Things;
using System.Collections.Generic;
using frog.Screens.Util;

namespace frog.Screens
{
    public class OccupationScreen : IStage
    {
        public delegate OccupationScreen Factory();

        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Texture2D _occupationTexture;
        private Texture2D _oval;
        private Texture2D _nextButton;
        private MouseState _lastMouseState;

        private ContentManager _contentManager;
        private GameState _gameState;
        private GraphicsDevice _graphicsDevice;
        private VillaScreen.Factory _villaScreenFactory;

        private List<Button> _occupationButtons = new List<Button>();

        private bool _readyButtonHovered;

        // todo
        string nanny = "nanny";
        string barber = "barber";
        string electrician = "electrician";
        string ceramicist = "ceramicist";
        string dryCleaner = "dry cleaner";
        string astronomer = "astronomer";
        string operaSinger = "opera singer";
        string runwayModel = "runway model";
        string streamer = "streamer";
        string trustFundBabie = "trust fund babie";

        public OccupationScreen(SpriteBatch spriteBatch, SpriteFont font, ContentManager contentManager, GameState gameState, GraphicsDevice graphicsDevice, VillaScreen.Factory villaScreenFactory)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _contentManager = contentManager;
            _gameState = gameState;
            _villaScreenFactory = villaScreenFactory;
            _graphicsDevice = graphicsDevice;

            _occupationTexture = _contentManager.Load<Texture2D>("occupationScreen");
            _oval = _contentManager.Load<Texture2D>("purpleOval");
            _nextButton = _contentManager.Load<Texture2D>("nextArrow");

            this.initButton(120, 145, nanny);
            this.initButton(350, 270, barber);
            this.initButton(100, 270, electrician);
            this.initButton(155, 520, ceramicist);
            this.initButton(355, 170, dryCleaner);
            this.initButton(600, 260, astronomer);
            this.initButton(530, 470, operaSinger);
            this.initButton(195, 400, runwayModel);
            this.initButton(635, 165, streamer);
            this.initButton(590, 370, trustFundBabie);
        }

        public void Draw()
        {
            _spriteBatch.Draw(_occupationTexture, new Vector2(0, 0), Color.AliceBlue);

            // Place text and ovals
            foreach (var button in _occupationButtons)
            {
                this.drawButton(button);
            }

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

        private void initButton(int x, int y, string label)
        {
            _occupationButtons.Add(new Button(x,
                                              y,
                                              _oval,
                                              label,
                                              _font.MeasureString(label)));
        }

        private void drawButton(Button button)
        {
            _spriteBatch.Draw(_oval, button.Viewport, Color.AliceBlue);
            _spriteBatch.DrawString(_font,
                button.Label,
                button.LabelOrigin,
                Color.White,
                0,
                _font.MeasureString(button.Label) / 2,
                1.0f,
                SpriteEffects.None,
                0.5f);
        }

        public void UpdateClick(MouseState mouseState)
        {
            // handle the buttons being pressed
            //Occupation chosen = ;

            // ready button
            if (mouseState.Y > 503 && mouseState.Y < 572)
            {
                if (mouseState.X > 521 && mouseState.X < 779)
                {
                    _gameState.Player.Occupation = new Occupation("test");
                    _gameState.CurrentStage = _villaScreenFactory();
                }
            }
        }

        public void UpdateHover(MouseState mouseState)
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

            foreach (var button in _occupationButtons)
            {
                button.SetHoverState(mouseState);
            }
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
        }
    }
}
