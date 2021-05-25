using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AstralChartGame
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ControlesState : Page
    {
        bool narrator;
        string source;
        MediaElement mediaElement = new MediaElement();
        public ControlesState()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (OptionsStateParameters)e.Parameter;
            narrator = parameters.narrat;
            BG_Image.Source = new BitmapImage(new Uri(parameters.BGSource));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
            if (narrator)
                speak("Pulsaste Volver");
        }
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
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

        private void onEnteredButton(object sender, PointerRoutedEventArgs e)
        {
            if (narrator)
                speak("Estas sobre Volver");
        }
    }
}
