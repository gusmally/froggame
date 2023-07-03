using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frog.game.Screens.Text
{
    public class HintDisplay : TextDisplay
    {
        public delegate HintDisplay Factory(Vector2 location);

        public HintDisplay(SpriteBatch spriteBatch, SpriteFont font, Vector2 location) : base(spriteBatch, font, location)
        {
            
        }
    }
}
