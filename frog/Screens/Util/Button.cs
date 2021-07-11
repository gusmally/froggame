using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens.Util
{
    public class Button
    {
        private const int _hoverMagnitude = 5;
        private const int _buffer = 50;
        private const int _halfbuffer = _buffer / 2;

        private Vector2 _origin;
        private Vector2 _hoveredOrigin;
        private Vector2 _labelDimensions;
        private Vector2 _hoveredLabelDimensions;
        private Rectangle _viewport;
        private Rectangle _hoveredViewport;
        private bool _isHovered;

        public Vector2 Origin => _isHovered ? _hoveredOrigin : _origin;
        public Vector2 LabelOrigin => _isHovered ? _hoveredLabelDimensions : _labelDimensions;
        public Rectangle Viewport => _isHovered ? _hoveredViewport : _viewport;
        public Texture2D Texture { get; }
        public string Label { get; }

        public bool HasBeenClicked { get; set; }

        // todo: make hover magnitude a parameter
        public Button(int x, int y, Texture2D texture, string label, Vector2 labelDimensions)
        {
            _origin = new Vector2(x, y);
            _hoveredOrigin = new Vector2(_origin.X + _hoverMagnitude, _origin.Y + _hoverMagnitude);

            _viewport = new Rectangle(x - (int)(labelDimensions.X / 2),
                                      y - (int)(labelDimensions.Y / 1.3),
                                      (int)labelDimensions.X + _buffer,
                                      70);
            _hoveredViewport = new Rectangle(_viewport.X + _hoverMagnitude,
                                             _viewport.Y + _hoverMagnitude,
                                             _viewport.Width,
                                             _viewport.Height);

            _labelDimensions = new Vector2(x + _halfbuffer, y);
            _hoveredLabelDimensions = new Vector2(_labelDimensions.X + _hoverMagnitude, _labelDimensions.Y + _hoverMagnitude);

            this.Texture = texture;
            this.Label = label;
        }

        public void SetHoverState(MouseState mouseState)
        {
            _isHovered = mouseState.X > Viewport.X &&
                 mouseState.X < Viewport.X + Viewport.Width &&
                 mouseState.Y > Viewport.Y &&
                 mouseState.Y < Viewport.Y + Viewport.Height;
        }

        public void SetHasBeenClicked(MouseState mouseState)
        {
            this.HasBeenClicked = mouseState.LeftButton == ButtonState.Pressed &&
                mouseState.X > Viewport.X &&
                mouseState.X < Viewport.X + Viewport.Width &&
                mouseState.Y > Viewport.Y &&
                mouseState.Y < Viewport.Y + Viewport.Height;
        }
    }
}
