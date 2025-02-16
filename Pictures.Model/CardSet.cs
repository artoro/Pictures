namespace Pictures.Model
{
    /// <summary>
    /// Enum reprezentuje dopuszczalne wielkości zestawu dobieranych kart.
    /// </summary>
    public enum CardSetSize
    {
        Size15 = 15,
        Size16 = 16,
    }

    /// <summary>
    /// Klasa zawierająca karty dobrane z talii i wyłożone na stole w czasie aktualnej rundy gry.
    /// </summary>
    public class CardSet
    {
        public List<Picture> Deck;
        public Picture[] Pictures { get; private set; }
        public int Size => Pictures.Length;
        public Shuffle Shuffle { get; private set; }

        public CardSet(ICollection<Picture> deck, int size)
        {
            Deck = deck.ToList();
            Pictures = new Picture[size];
            Shuffle = new Shuffle();
            DrawNewSet();
        }

        public virtual void DrawNewSet()
        {
            int firstCard = Shuffle.Next(Deck.Count);
            for (int drawCard = 0; drawCard < Pictures.Length; drawCard++)
            {
                int card = (firstCard + drawCard) % Deck.Count;
                Pictures[drawCard] = Deck[card];
            }
        }

        public bool RemoveFromDeck()
        {
            bool deckIsEmpty = false;
            foreach (Picture picture in Pictures)
            {
                if (!Deck.Remove(picture))
                {
                    deckIsEmpty = true;
                    break;
                }
            }
            if (deckIsEmpty) Deck.Clear();
            return deckIsEmpty;
        }
    }
}
