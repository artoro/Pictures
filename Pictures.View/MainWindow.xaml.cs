using Pictures.View.Animations;
using Pictures.ViewModel;
using Pictures.ViewModel.Utils;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Pictures.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var menuStart = new MenuStart();
        var menuStartViewModel = menuStart.DataContext as MenuStartViewModel;
        menuStartViewModel.NewGameCommand = new RelayCommand((o) => AddAnimatedPicture());
        DataContext = new MainViewModel(menuStart);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 8; i++) // Można zmienić liczbę obrazów
        {
            AddAnimatedPicture();
        }
    }

    private Random _random = new Random();
    private void AddAnimatedPicture()
    {
        // Tworzymy PictureCard i ustawiamy źródło obrazu (można bindować do ViewModelu)
        double scale = MainContainer.ActualWidth * 0.25;
        PictureCard pictureCard = new PictureCard
        {
            Width = scale,
            Height = scale * 0.7,
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(10, 10),  // Start od powiększonego obrazu
                    new RotateTransform(0),
                    new TranslateTransform(
                        _random.Next((int)-Width, (int)Width),
                        _random.Next((int)-Height, (int)Height)) // Start z góry ekranu
                }
            }
        };


        // Generujemy losową pozycję docelową
        double targetX = 1.1 * (0.5 - _random.NextDouble()) * (MainContainer.ActualWidth);
        double targetY = 1.1 * (0.5 - _random.NextDouble()) * (MainContainer.ActualHeight);
        double rotationAngle = _random.Next(-60, 60); // Losowy obrót

        CardCollisionChecker collisionChecker = new CardCollisionChecker(2.2 * scale, 2.2 * scale * 0.7);
        if (!cards.Any(c => collisionChecker.AreRectanglesOverlapping(c, new Vector2((float)targetX, (float)targetY), rotationAngle)))
        {
            cards.Add(new Vector2((float)targetX, (float)targetY));
            MainContainer.Children.Add(pictureCard);
            AnimatePicture(pictureCard, targetX, targetY, rotationAngle);
        }
    }
    List<Vector2> cards = new List<Vector2>() { new Vector2(0, 0) };


    private void AnimatePicture(PictureCard picture, double targetX, double targetY, double angle)
    {
        TransformGroup transformGroup = (TransformGroup)picture.RenderTransform;
        ScaleTransform scale = (ScaleTransform)transformGroup.Children[0];
        RotateTransform rotate = (RotateTransform)transformGroup.Children[1];
        TranslateTransform translate = (TranslateTransform)transformGroup.Children[2];

        Storyboard storyboard = new Storyboard();

        // Animacja Opacity (0 -> 1 w 1 sekundę)
        DoubleAnimation opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
        Storyboard.SetTarget(opacityAnim, picture.Test);
        Storyboard.SetTargetProperty(opacityAnim, new PropertyPath("Opacity"));

        // Animacja Skali (10 -> 1 w 5 sekund)
        DoubleAnimation scaleXAnim = new DoubleAnimation(10, 1, TimeSpan.FromSeconds(3));
        DoubleAnimation scaleYAnim = new DoubleAnimation(10, 1, TimeSpan.FromSeconds(3));
        Storyboard.SetTarget(scaleXAnim, picture);
        Storyboard.SetTarget(scaleYAnim, picture);
        Storyboard.SetTargetProperty(scaleXAnim, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
        Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

        //// Animacja Położenia
        DoubleAnimation translateXAnim = new DoubleAnimation(0, targetX, TimeSpan.FromSeconds(3));
        DoubleAnimation translateYAnim = new DoubleAnimation(0, targetY, TimeSpan.FromSeconds(3));
        Storyboard.SetTarget(translateXAnim, picture);
        Storyboard.SetTarget(translateYAnim, picture);
        Storyboard.SetTargetProperty(translateXAnim, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(TranslateTransform.X)"));
        Storyboard.SetTargetProperty(translateYAnim, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(TranslateTransform.Y)"));

        //// Animacja Obrotu (-30° do 30° w 5 sekund)
        DoubleAnimation rotateAnim = new DoubleAnimation(0, angle, TimeSpan.FromSeconds(3));
        Storyboard.SetTarget(rotateAnim, picture);
        Storyboard.SetTargetProperty(rotateAnim, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

        // Dodanie animacji do Storyboarda i uruchomienie
        storyboard.Children.Add(opacityAnim);
        storyboard.Children.Add(scaleXAnim);
        storyboard.Children.Add(scaleYAnim);
        storyboard.Children.Add(translateXAnim);
        storyboard.Children.Add(translateYAnim);
        storyboard.Children.Add(rotateAnim);
        storyboard.Begin();
    }
}