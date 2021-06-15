using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frog.Screens.Util
{
    public class Button
    {
        public Vector2 Origin { get; }
        public Rectangle Viewport { get; }
        public Texture2D Texture { get; }
        public string Label { get; }
        public bool IsHovered { get; set; }
        public bool IsDepressed { get; set; }

        public Button(Vector2 origin, Rectangle viewport, Texture2D texture, string label)
        {
            this.Origin = origin;
            this.Viewport = viewport;
            this.Texture = texture;
            this.Label = label;
        }
    }
}
