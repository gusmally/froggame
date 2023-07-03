using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frog.game.Screens.Text
{
    public class TextDisplay
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Vector2 _location;

        public TextDisplay(SpriteBatch spriteBatch, SpriteFont font, Vector2 location)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _location = location;
        }

        public void Draw(string text)
        {
            _spriteBatch.DrawString(_font,
                text,
                _location,
                Color.White,
                0,
                new Vector2(0, 0),
                0.5f,
                SpriteEffects.None,
                0.5f);
        }
    }
}
