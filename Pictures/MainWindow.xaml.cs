using System.Windows;
using Pictures.ViewModel;

namespace Pictures
{
    public partial class MainWindow : Window
    {
        private BoardViewModel gameViewModel;

        public bool IsFullScreen
        {
            get;
            set;
        }
        //private bool isFullScreen = false;

        public MainWindow()
        {
            gameViewModel = new BoardViewModel();
            DataContext = gameViewModel;

            InitializeComponent();
            ChangeDifficultLevelToEasy(null, null);

            var editDeckPage = new Game15Page();
            editDeckPage.DataContext = gameViewModel;
            MainFrame.Navigate(editDeckPage);
        }

        private void ChangeDifficultLevelToEasy(object sender, RoutedEventArgs e)
        {
            gameViewModel.ChangeDifficultLevelCommand.Execute(DifficultLevels.Easy);
            DifficultyLevelEasy.IsChecked = true;
            DifficultyLevelMedium.IsChecked = false;
            DifficultyLevelHard.IsChecked = false;
        }

        private void ChangeDifficultLevelToMedium(object sender, RoutedEventArgs e)
        {
            gameViewModel.ChangeDifficultLevelCommand.Execute(DifficultLevels.Medium);
            DifficultyLevelEasy.IsChecked = false;
            DifficultyLevelMedium.IsChecked = true;
            DifficultyLevelHard.IsChecked = false;
        }

        private void ChangeDifficultLevelToHard(object sender, RoutedEventArgs e)
        {
            gameViewModel.ChangeDifficultLevelCommand.Execute(DifficultLevels.Hard);
            DifficultyLevelEasy.IsChecked = false;
            DifficultyLevelMedium.IsChecked = false;
            DifficultyLevelHard.IsChecked = true;
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}