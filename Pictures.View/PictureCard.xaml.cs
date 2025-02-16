using Pictures.ViewModel;
using System.Windows.Controls;

namespace Pictures.View
{
    /// <summary>
    /// Interaction logic for PictureCard.xaml
    /// </summary>
    public partial class PictureCard : UserControl
    {
        public PictureCard()
        {
            InitializeComponent();
            DataContext = new PictureCardViewModel();
        }
    }
}
