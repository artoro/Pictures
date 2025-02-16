using Pictures.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Pictures.View
{
    /// <summary>
    /// Interaction logic for MenuStart.xaml
    /// </summary>
    public partial class MenuStart : UserControl
    {
        public MenuStart()
        {
            InitializeComponent();
            DataContext = new MenuStartViewModel();
        }
    }
}
