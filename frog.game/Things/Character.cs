using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Things
{
    public class Character
    {
        public delegate Character Factory(string name,
                         Pronoun pronoun,
                         Texture2D smallSprite,
                         Texture2D largeSprite,
                         Occupation occupation = null);

        public string Name { get; }
        public (Pronoun main, Dictionary<PronounType, string> dict) Pronouns { get; set; }
        public Texture2D SmallSprite { get; }
        public Texture2D LargeSprite { get; }
        public Occupation Occupation { get; set; }
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

        public Vector2 Position;
        public float Speed => _frogSpeed;

        private const float _frogSpeed = 100f;
        private int _preferredBackBufferWidth;
        private int _preferredBackBufferHeight;
        private SpriteBatch _spriteBatch;

        public Character(string name,
                         Pronoun pronoun,
                         Texture2D smallSprite,
                         Texture2D largeSprite,
                         GraphicsDeviceManager graphicsDeviceManager,
                         SpriteBatch spriteBatch,
                         Occupation occupation = null)
        {
            this.Name = name;
            this.SmallSprite = smallSprite;
            this.LargeSprite = largeSprite;
            this.Position = Vector2.Zero;
            this.Occupation = occupation;
            _preferredBackBufferWidth = graphicsDeviceManager.PreferredBackBufferWidth;
            _preferredBackBufferHeight = graphicsDeviceManager.PreferredBackBufferHeight;
            _spriteBatch = spriteBatch;

            switch (pronoun)
            {
                case Pronoun.He:
                    var hePronouns = new Dictionary<PronounType, string>();
                    hePronouns.Add(PronounType.Subject, "he");
                    hePronouns.Add(PronounType.Object, "him");
                    hePronouns.Add(PronounType.PossessiveAdjective, "his");
                    hePronouns.Add(PronounType.Possessive, "his");
                    hePronouns.Add(PronounType.Reflexive, "himself");
                    this.Pronouns = (Pronoun.He, hePronouns);
                    break;
                case Pronoun.She:
                    var shePronouns = new Dictionary<PronounType, string>();
                    shePronouns.Add(PronounType.Subject, "she");
                    shePronouns.Add(PronounType.Object, "her");
                    shePronouns.Add(PronounType.PossessiveAdjective, "her");
                    shePronouns.Add(PronounType.Possessive, "hers");
                    shePronouns.Add(PronounType.Reflexive, "herself");
                    this.Pronouns = (Pronoun.She, shePronouns);
                    break;
                case Pronoun.They:
                    var theyPronouns = new Dictionary<PronounType, string>();
                    theyPronouns.Add(PronounType.Subject, "they");
                    theyPronouns.Add(PronounType.Object, "them");
                    theyPronouns.Add(PronounType.PossessiveAdjective, "their");
                    theyPronouns.Add(PronounType.Possessive, "theirs");
                    theyPronouns.Add(PronounType.Reflexive, "themself");
                    this.Pronouns = (Pronoun.They, theyPronouns);
                    break;
            }
        }

        public void Draw()
        {
            _spriteBatch.Draw(this.SmallSprite, this.Position, null, Color.White, 0f,
                              new Vector2(this.SmallSprite.Width / 2, this.SmallSprite.Height / 2),
                              new Vector2(2, 2),
                              this.SpriteEffects,
                              0f);
        }

        public void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
                this.Position.Y -= _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down))
                this.Position.Y += _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                this.Position.X -= _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.SpriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                this.Position.X += _frogSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.SpriteEffects = SpriteEffects.None;
            }

            if (this.Position.X > _preferredBackBufferWidth - this.SmallSprite.Width / 2)
            {
                this.Position.X = _preferredBackBufferWidth - this.SmallSprite.Width / 2;
            }
            else if (this.Position.X < this.SmallSprite.Width / 2)
            {
                this.Position.X = this.SmallSprite.Width / 2;
            }

            if (this.Position.Y > _preferredBackBufferHeight - this.SmallSprite.Height / 2)
            {
                this.Position.Y = _preferredBackBufferHeight - this.SmallSprite.Height / 2;
            }
            else if (this.Position.Y < this.SmallSprite.Height / 2)
            {
                this.Position.Y = this.SmallSprite.Height / 2;
            }
        }
    }
}
