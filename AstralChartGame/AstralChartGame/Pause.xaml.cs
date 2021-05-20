using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AstralChartGame
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Pause : Page
    {
        MediaElement mediaElement = new MediaElement();

        public Pause()
        {
            this.InitializeComponent();
        }
        private void onButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string code = button.Tag.ToString();
            string option;

            mediaElement.Stop();

            switch (code)
            {
                case "Exit": {
                        CoreApplication.Exit();
                    }
                    break;
                case "Options": {
                        Frame.Navigate(typeof(OptionsState));
                    }
                    break;
                case "Resume": {
                        Frame.Navigate(typeof(GameState));
                    }
                    break;    
                case "Menu": {
                        Frame.Navigate(typeof(Menu));
                    }
                    break;
                default:
                    break;
            }

            option = "You pressed" + code;

            speak(option);
        }

        private async void onButtonOver(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string code = button.Tag.ToString();

            string option = "You are on top of" + code;

            speak(option);
        }

        private void onButtonExited(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private async void speak(string option)
        {
            //mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream =
                await synth.SynthesizeTextToStreamAsync(option);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
