namespace frog
{
    public class GameState
    {
        public GameStage Stage { get; set; }
        public enum GameStage { TitleScreen, CharacterCreation, Occupation, MainGame };

        public GameState()
        {
            Stage = GameStage.TitleScreen;
        }
    }
}
