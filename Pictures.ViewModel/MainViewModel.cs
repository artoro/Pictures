using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Pictures.ViewModel.Utils;

namespace Pictures.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ChangeViewCommand { get; }

        public MainViewModel(UserControl currentView)
        {
            CurrentView = currentView;
            ChangeViewCommand = new RelayCommand(ChangeView);
        }

        private void ChangeView(object view)
        {
            CurrentView = view as UserControl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
