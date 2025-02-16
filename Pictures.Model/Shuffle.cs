namespace Pictures.Model
{
    /// <summary>
    /// Klasa łącząca GameSeed z klasą System.Random.
    /// </summary>
    public class Shuffle
    {
        public GameSeed Seed
        {
            get => seed;
            set
            {
                seed = value;
                random = new Random(seed.Value);
            }
        }
        private GameSeed seed;

        private int debugHistory; // zmienna testowa
        private Random random;

        public void Reset()
        {
            debugHistory = 0;
            random = new Random(seed.Value);
        }

        public int Next(int deckSize)
        {
            debugHistory++;
            return random.Next(deckSize);
        }

        public Shuffle() : this(String.Empty) { }

        public Shuffle(string seedCode)
        {
            if (String.IsNullOrEmpty(seedCode)) Seed = GameSeed.Generate();
            else Seed = new GameSeed(seedCode);
        }

        public Shuffle(GameSeed seed)
        {
            Seed = seed;
        }
    }
}
