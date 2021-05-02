using Microsoft.Xna.Framework.Input;

namespace frog.Screens
{
    public interface IStage
    {
        void Draw();
        void Update(MouseState mouseState);
    }
}
