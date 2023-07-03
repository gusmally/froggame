using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frog.game.Screens.Text
{
    // todo: this and hint display could be combined and just instantiated with different values 
    // if there isn't anything long-term separating them
    public class DialogDisplay : TextDisplay
    {
        public delegate DialogDisplay Factory(Vector2 location);

        public DialogDisplay(SpriteBatch spriteBatch, SpriteFont font, Vector2 location) : base(spriteBatch, font, location)
        {

        }
    }
}