namespace Pictures.Model
{
    /// <summary>
    /// Klasa ustawień rozgrywki.
    /// </summary>
    public class GameSettings
    {
        public GameSeed GameSeed { get; set; } = GameSeed.Generate();


        public int FinalRundNumber
        {
            get => finalRundNumber;
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException();
                else finalRundNumber = value;
            }
        }
        private int finalRundNumber = 5;


        public CardSetSize CardSetSize { get; set; } = CardSetSize.Size15;


        public DifficultyLevel DifficultyLevel { get; set; } = new MediumLevel();


        public GameSettings(GameSettings settings)
        {
            this.GameSeed = settings.GameSeed;
            this.FinalRundNumber = settings.FinalRundNumber;
            this.CardSetSize = settings.CardSetSize;
            this.DifficultyLevel = settings.DifficultyLevel;
        }
    }
}
