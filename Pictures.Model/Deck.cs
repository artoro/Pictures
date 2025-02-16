namespace Pictures.Model
{
    /// <summary>
    /// Klasa statyczna służy do przechowywania w pamięci komputera oryginalnej kopii talii.
    /// Instancje klasy zawierają aktualną talię kart oraz stos kart wykorzystanych.
    /// </summary>
    public class Deck
    {
        private List<Picture> cards;
        private GameSeed seed;

        public Deck(Deck deck, GameSeed gameSeed)
        {
            cards = deck.cards.ToList();
            seed = gameSeed;
        }
    }
}
