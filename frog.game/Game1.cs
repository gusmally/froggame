using System.Collections.Generic;
using Autofac;
using frog.Screens;
using frog.Things;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog
{
    public partial class Game1 : Game
    {
        public delegate Game1 Factory();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState _lastMouseState;
        private IContainer _container;

        private List<Character> _characters = new List<Character>();
        private GameState _gameState;

        private TitleScreen.Factory _titleScreenFactory;
        private Character.Factory _characterFactory;

        public Game1()
        {
            this.bootstrap();

            _titleScreenFactory = _container.Resolve<TitleScreen.Factory>();
            _gameState = _container.Resolve<GameState>();
            _graphics = _container.Resolve<GraphicsDeviceManager>();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GameState>().SingleInstance();
            builder.RegisterType<TitleScreen>().SingleInstance();
            builder.RegisterType<CharacterScreen>().SingleInstance();
            builder.RegisterType<OccupationScreen>().SingleInstance();
            builder.RegisterType<OutsideScreen>().SingleInstance();
            builder.RegisterType<VillaScreen>().SingleInstance();
            builder.RegisterType<SpriteBatch>().SingleInstance();
            builder.RegisterType<Character>().SingleInstance();
            builder.Register((c, p) => this.Content.Load<SpriteFont>("dearLovely")).As<SpriteFont>();
            builder.RegisterInstance(this.Content).As<ContentManager>().SingleInstance();
            builder.Register((c, p) => new GraphicsDeviceManager(this)).As<GraphicsDeviceManager>().SingleInstance();
            builder.Register((c, p) => this.GraphicsDevice).As<GraphicsDevice>().SingleInstance();
            _container = builder.Build();
        }

        protected override void Initialize()
        {
            _spriteBatch = _container.Resolve<SpriteBatch>(new List<NamedParameter>(){ new NamedParameter("graphicsDevice", this.GraphicsDevice)});
            _characterFactory = _container.Resolve<Character.Factory>();

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            _gameState.CurrentStage = _titleScreenFactory();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // font
            _container.Resolve<SpriteFont>(); 

            // characters
            _characters.Add(_characterFactory("Ash", Pronoun.They, this.Content.Load<Texture2D>("ash"), this.Content.Load<Texture2D>("big_ash"), new Occupation("fashion")));
            _characters.Add(_characterFactory("Bigstick", Pronoun.They, this.Content.Load<Texture2D>("bigstick"), this.Content.Load<Texture2D>("big_bigstick"), new Occupation("cycling")));
            _characters.Add(_characterFactory("Cubi", Pronoun.They, this.Content.Load<Texture2D>("cubi"), this.Content.Load<Texture2D>("big_cubi"), new Occupation("plumber")));
            _characters.Add(_characterFactory("Juicebox", Pronoun.They, this.Content.Load<Texture2D>("juicebox"), this.Content.Load<Texture2D>("big_juicebox"), new Occupation("glassblower")));
            _characters.Add(_characterFactory("Kate", Pronoun.They, this.Content.Load<Texture2D>("kate"), this.Content.Load<Texture2D>("big_kate"), new Occupation("makeup")));
            _characters.Add(_characterFactory("Lastone", Pronoun.They, this.Content.Load<Texture2D>("lastone"), this.Content.Load<Texture2D>("big_lastone"), new Occupation("dj")));
            _characters.Add(_characterFactory("Mike", Pronoun.They, this.Content.Load<Texture2D>("mike"), this.Content.Load<Texture2D>("big_mike"), new Occupation("dancer")));
            _characters.Add(_characterFactory("Omar", Pronoun.They, this.Content.Load<Texture2D>("omar"), this.Content.Load<Texture2D>("big_omar"), new Occupation("runescape 2007")));
            _characters.Add(_characterFactory("Pea", Pronoun.They, this.Content.Load<Texture2D>("pea"), this.Content.Load<Texture2D>("big_pea"), new Occupation("baking")));
            _characters.Add(_characterFactory("Shim", Pronoun.They, this.Content.Load<Texture2D>("shim"), this.Content.Load<Texture2D>("big_shim"), new Occupation("gardening")));
            _characters.Add(_characterFactory("Tode", Pronoun.They, this.Content.Load<Texture2D>("tode"), this.Content.Load<Texture2D>("big_tode"), new Occupation("software engineer")));
            _characters.Add(_characterFactory("Willow", Pronoun.They, this.Content.Load<Texture2D>("willow"), this.Content.Load<Texture2D>("big_willow"), new Occupation("jewellery mlm")));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            _gameState.CurrentStage.UpdateHover(mouseState);
            _gameState.CurrentStage.UpdateKeyboard(keyboardState, gameTime);

            // return if not clicking
            if (mouseState.LeftButton == _lastMouseState.LeftButton)
                return;

            _lastMouseState = mouseState;

            if (mouseState.LeftButton == ButtonState.Released)
                return;

            _gameState.CurrentStage.UpdateClick(mouseState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _gameState.CurrentStage.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
