using Pictures.ViewModel;
using System.Windows.Controls;

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
