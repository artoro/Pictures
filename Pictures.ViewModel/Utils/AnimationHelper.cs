using System.Windows;
using System.Windows.Media.Animation;

namespace Pictures.ViewModel.Utils
{
    public static class AnimationHelper
    {
        public static void PlayStoryboard(UIElement element, string storyboardKey)
        {
            if (element == null) return;

            // Pobieramy animację ze słownika zasobów
            var storyboard = Application.Current.TryFindResource(storyboardKey) as Storyboard;
            storyboard?.Begin((FrameworkElement)element);
        }
    }
}
