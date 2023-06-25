using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public interface IStage
    {
        void Draw();
        void UpdateClick(MouseState mouseState);
        void UpdateHover(MouseState mouseState);
        void UpdateKeyboard(KeyboardState keyboardState, GameTime gameTime);
    }
}
