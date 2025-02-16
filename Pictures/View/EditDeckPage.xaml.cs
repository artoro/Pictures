using Microsoft.Win32;
using Pictures.Data;
using Pictures.Model;
using Pictures.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Pictures
{
    /// <summary>
    /// Interaction logic for EditDeckPage.xaml
    /// </summary>
    public partial class EditDeckPage : Page
    {
        public EditDeckPage()
        {
            InitializeComponent();
            LoadPictures();
        }

        private Picture[] pictures;
        private XmlSerializer serializer = new XmlSerializer(typeof(Picture[]));

        private void LoadPictures()
        {
            if (File.Exists(PicturesSerializer.LocalDatabaseFilePath))
            {
                pictures = PicturesSerializer.DeserializePictures();
            }
            else
            {
                pictures = new Picture[0];
            }

            PicturesDataGrid.ItemsSource = pictures;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select database file to load";
            openFileDialog.Filter = "XML files (*.xml)|*.xml|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result == true &&
                File.Exists(openFileDialog.FileName))
            {
                pictures = PicturesSerializer.DeserializePictures(openFileDialog.FileName);
            }
            else
            {
                pictures = new Picture[0];
            }

            PicturesDataGrid.ItemsSource = pictures;
            //PicturesDataGrid.UpdateLayout();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newPictures = new Picture[pictures.Length + 1];
            pictures.CopyTo(newPictures, 0);
            newPictures[pictures.Length] = new Picture()
            {
                ID = newPictures.Length,
                Tags = "default"
            };
            pictures = newPictures;

            PicturesDataGrid.ItemsSource = null;
            PicturesDataGrid.ItemsSource = pictures;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Usuń zaznaczone obiekty Picture z tablicy
            foreach (var item in PicturesDataGrid.SelectedItems)
            {
                var picture = (Picture)item;
                pictures = pictures.Where(p => p != picture).ToArray();
            }
            for (int i = 0; i<pictures.Length; i++)
            {
                pictures[i].ID = i;
            }

            PicturesDataGrid.ItemsSource = null;
            PicturesDataGrid.ItemsSource = pictures;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PicturesSerializer.SerializePictures(pictures);
            ((BoardViewModel)this.DataContext).MainDeckInit();
        }
    }
}
