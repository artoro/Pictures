using Pictures.Data;
using Pictures.Model;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pictures.ViewModel
{
    public enum DifficultLevels { Easy, Medium, Hard };

    public class BoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Picture[] mainDeck;
        public void MainDeckInit()
        {
            mainDeck = PicturesSerializer.DeserializePictures();
        }
        private void NewGame(object parameter)
        {
            rundNumber = 0;
            GetNextCards(null);
        }

        public BitmapImage[] Cards { get => cards; }
        private BitmapImage[] cards = new BitmapImage[16];
        private void GetNextCards(object parameter)
        {
            Random r = new Random();
            int i = r.Next(0,10);
            for (int card = 0; card < cards.Length; card++)
            {
                i = (i + 1) % mainDeck.Length;
                BitmapImage bitmap = new();
                bitmap.BeginInit();
                bitmap.UriSource = mainDeck[i].Uri;
                bitmap.EndInit();
                cards[card] = bitmap;
            }
            rundNumber++;
            OnPropertyChanged(nameof(RundNumber));
            OnPropertyChanged(nameof(Cards));
        }

        public int RundNumber
        {
            get => rundNumber;
        }
        private int rundNumber = 1;

        public int MaxRundNumber
        {
            get => maxRundNumber;
            private set
            {
                if (value != maxRundNumber &&
                    value >= 1)
                {
                    maxRundNumber = value;
                    OnPropertyChanged(nameof(MaxRundNumber));
                }
            }
        }
        private int maxRundNumber = 5;

        public DifficultLevels DifficultLevel { get; set; }

        public RelayCommand ChangeDifficultLevelCommand { get; }

        public RelayCommand IncreaseRundNumberCommand { get; }
        public RelayCommand SetDefaultRundNumberCommand { get; }
        public RelayCommand DecreaseRundNumberCommand { get; }

        public RelayCommand NewGameCommand { get; }
        public RelayCommand NextRundCommand { get; }

        public BoardViewModel()
        {
            MainDeckInit();
            cards = new BitmapImage[16];

            ChangeDifficultLevelCommand = new RelayCommand((level) => DifficultLevel = (DifficultLevels)level);

            IncreaseRundNumberCommand = new RelayCommand((o) => MaxRundNumber++);
            SetDefaultRundNumberCommand = new RelayCommand((o) => MaxRundNumber = 5, (o) => MaxRundNumber != 5);
            DecreaseRundNumberCommand = new RelayCommand((o) => MaxRundNumber--, (o) => MaxRundNumber > 1);

            NewGameCommand = new RelayCommand(NewGame);
            NextRundCommand = new RelayCommand(GetNextCards, (o) => MaxRundNumber > rundNumber);
        }

    }
}
