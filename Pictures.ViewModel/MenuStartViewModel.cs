using Pictures.ViewModel.Utils;
using System.Windows;
using System.Windows.Input;

namespace Pictures.ViewModel
{
    public class MenuStartViewModel
    {
        public ICommand NewGameCommand { get; }
        public ICommand JoinGameCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand RulesCommand { get; }

        public MenuStartViewModel()
        {
            NewGameCommand = new RelayCommand(ExecuteNewGame);
            JoinGameCommand = new RelayCommand(ExecuteJoinGame);
            SettingsCommand = new RelayCommand(ExecuteSettings);
            RulesCommand = new RelayCommand(ExecuteRules);
        }

        private void ExecuteNewGame(object obj)
        {
            Console.WriteLine("Nowa gra - w trakcie implementacji");
        }

        private void ExecuteJoinGame(object obj)
        {
            Console.WriteLine("Dołącz do gry - w trakcie implementacji");
        }

        private void ExecuteSettings(object obj)
        {
            Console.WriteLine("Ustawienia - w trakcie implementacji");
        }

        private void ExecuteRules(object obj)
        {
            Console.WriteLine("Zasady gry - w trakcie implementacji");
        }
    }
}
