using Pictures.Model;
using System;
using System.Windows.Media.Imaging;

namespace Pictures.ViewModel
{
    public class PictureCardViewModel
    {
        public BitmapImage PictureImage => new BitmapImage(picture.Uri);
        private Picture picture = new Picture();
    }
}
