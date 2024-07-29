using Microsoft.UI.Xaml;
using System.IO;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace prueba
{
    public sealed partial class MainPage : Page
    {
        private double playerPosition = 0;
        private const double step = 10;
        private Windows.Media.Playback.MediaPlayer mediaPlayer;
        private bool isMoving = false;
        private bool isPlaying = false;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += Page_Loaded;
            mediaPlayer = new Windows.Media.Playback.MediaPlayer();
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus(FocusState.Programmatic);

            PlayerCanvas.SizeChanged += (s, args) =>
            {
                Canvas.SetLeft(PlayerImage, (PlayerCanvas.ActualWidth - PlayerImage.ActualWidth) / 2);
                playerPosition = Canvas.GetLeft(PlayerImage);
            };
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/sounds/2nf-84831.mp3"));
            mediaPlayer.IsLoopingEnabled = true;   
        }
        
        private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            double previosPosition = playerPosition;
            if (e.Key.Equals(Windows.System.VirtualKey.A))
            {
                if (playerPosition > 0)
                {
                    playerPosition -= step;
                    Canvas.SetLeft(PlayerImage, playerPosition);

                }
            }
            else if (e.Key.Equals(Windows.System.VirtualKey.D))
            {
                if (playerPosition < PlayerCanvas.ActualWidth - PlayerImage.ActualWidth)
                {
                    playerPosition += step;
                    Canvas.SetLeft(PlayerImage, playerPosition);
               

                }
            }
            if (previosPosition != playerPosition)
            {

                Canvas.SetLeft(PlayerImage, playerPosition);
                if (!isMoving)
                {
                    mediaPlayer.Play();
                    isMoving = true;
                }
            }

        }

        private void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(Windows.System.VirtualKey.A) || e.Key.Equals(Windows.System.VirtualKey.D)){
                mediaPlayer.Pause();
                isMoving = false;
            }
        }
    }
}
