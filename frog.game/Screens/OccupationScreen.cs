﻿using System;
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
        private Texture2D _ovalHighlight;
        private Texture2D _nextButton;

        private ContentManager _contentManager;
        private GameState _gameState;
        private OutsideScreen.Factory _outsideScreenFactory;

        private List<Button> _occupationButtons = new List<Button>();

        private bool _nextButtonHovered;
        private bool _nextArrowVisible;
        private string _selectedOccupation;

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

        public OccupationScreen(SpriteBatch spriteBatch, SpriteFont font, ContentManager contentManager, GameState gameState, OutsideScreen.Factory outsideScreenFactory)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _contentManager = contentManager;
            _gameState = gameState;
            _outsideScreenFactory = outsideScreenFactory;

            _occupationTexture = _contentManager.Load<Texture2D>("occupationScreen");
            _oval = _contentManager.Load<Texture2D>("purpleOval");
            _ovalHighlight = _contentManager.Load<Texture2D>("ovalHighlight");
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
            if (_nextArrowVisible)
            {
                if (_nextButtonHovered)
                {
                    _spriteBatch.Draw(_nextButton, new Vector2(0, 15), Color.AliceBlue);
                }
                else
                {
                    _spriteBatch.Draw(_nextButton, new Vector2(0, 10), Color.AliceBlue);
                }
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
            if (button.HasBeenClicked)
            {
                _spriteBatch.Draw(_ovalHighlight,
                    new Rectangle(button.Viewport.X - 25, button.Viewport.Y -25, button.Viewport.Width + 50, button.Viewport.Height + 50),
                    Color.AliceBlue);
                _selectedOccupation = button.Label;
                _nextArrowVisible = true;
            }

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
            foreach (var button in _occupationButtons)
            {
                button.SetHasBeenClicked(mouseState);
            }

            // ready button
            if (mouseState.Y > 503 && mouseState.Y < 572)
            {
                if (mouseState.X > 521 && mouseState.X < 779)
                {
                    _gameState.Player.Occupation = new Occupation(_selectedOccupation);
                    _gameState.CurrentStage = _outsideScreenFactory();
                }
            }
        }

        public void UpdateHover(MouseState mouseState)
        {
            if (mouseState.Y > 503 && mouseState.Y < 572)
            {
                if (mouseState.X > 521 && mouseState.X < 779)
                {
                    _nextButtonHovered = true;
                }
            }
            else
            {
                _nextButtonHovered = false;
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
