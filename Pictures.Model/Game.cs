namespace Pictures.Model
{
    /// <summary>
    /// W momencie rozpoczęcia gry, należy stworzyć nowy obiekt, który będzie przechowywał klasy robocze i ustawienia gry.
    /// Ustawienia obowiązują od momentu utworzenia nowej instancji gry i nie można ich później zmieniać.
    /// Jeśli karty w talii się skończą, klasa nie odpowiada za ich przetasowanie.
    /// Jeśli gra dobiegnie końca (licznik rund dojdzie do finalnej wartości) i nastąpi próba rozegrania kolejnej rundy, zostanie wyrzucony wyjątek, który należy obsłużyć po stronie interfejsu użytkownika.
    /// </summary>
    public class Game
    {
        private GameSettings gameSettings;
        private Deck gameDeck;
        private CardDraw cardDraw;
        private CardSet cardSet;
        private int gameRound;

        public Game(GameSettings settings, Deck deck)
        {
            gameSettings = new GameSettings(settings);
            gameDeck = new Deck(deck, settings.GameSeed);
            cardDraw = new CardDraw(settings.CardSetSize, settings.DifficultyLevel);
            PrepareNextGame();
        }

        public void PrepareNextGame()
        {
            gameRound = 0;
            NextRund();
        }

        public void NextRund()
        {
            if (gameRound >= gameSettings.FinalRundNumber) throw new EndGameException();
            gameRound++;
            cardSet = cardDraw.PullSet(out gameDeck);
        }
    }

    public class EndGameException : Exception { }
}
