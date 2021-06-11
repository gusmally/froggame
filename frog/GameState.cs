using frog.Screens;
using frog.Things;

namespace frog
{
    public class GameState
    {
        public IStage CurrentStage { get; set; }

        public Character Player { get; set; }
        //public enum GameStage { TitleScreen, CharacterCreation, Occupation, MainGame };

        public GameState()
        {
        }
    }
}
