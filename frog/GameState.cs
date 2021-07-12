using frog.Screens;
using frog.Things;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace frog
{
    public class GameState
    {
        public IStage CurrentStage { get; set; }

        public Character Player { get; set; }

        public GameState()
        {
        }
    }
}
