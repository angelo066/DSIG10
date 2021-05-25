using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Globalization;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AstralChartGame
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class OptionsState : Page
    {
        int lang = 0;
        MediaPlayer musicPlayer, sfxPlayer; 
        MediaElement mediaElement;
        double mainVolume;
        double musicVolume;
        double sfxVolume;
        bool narrator = true;
        public OptionsState()
        {
            this.InitializeComponent();
            //Audio
            musicPlayer = new MediaPlayer();
            sfxPlayer = new MediaPlayer();
            mediaElement = new MediaElement();
            Init_BGMusic();

            this.NavigationCacheMode =
            Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            //Check Options states
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
                FullScreen_CheckBox.IsChecked = true;
            else
                FullScreen_CheckBox.IsChecked = false;

            if (narrator)
                NarratorBox.IsChecked = true;
            else
                NarratorBox.IsChecked = false;

            LanguageBox.SelectedIndex = lang;
        }
        #region Audio
        private async void speak(string option)
        {
            //mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream =
                await synth.SynthesizeTextToStreamAsync(option);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
        private async void Init_BGMusic()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\audios");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("BgMusic_NoCopy.mp3");

            musicPlayer.AutoPlay = true;
            musicPlayer.Source = MediaSource.CreateFromStorageFile(file);
            musicPlayer.IsLoopingEnabled = true;
        }
        private void changeMainVolume(object sender, RangeBaseValueChangedEventArgs e)
        {
            mainVolume = (sender as Slider).Value / 100;
            if (musicPlayer != null && sfxPlayer != null)
            {
                musicPlayer.Volume = mainVolume * musicVolume;
                sfxPlayer.Volume = mainVolume * sfxVolume;
            }
            if (narrator)
                speak((mainVolume * 100).ToString());
        }
        private void changeMusicVolume(object sender, RangeBaseValueChangedEventArgs e)
        {
            musicVolume = (sender as Slider).Value / 100;
            if (musicPlayer != null)
                musicPlayer.Volume = mainVolume * musicVolume;
            if (narrator)
                speak((musicVolume * 100).ToString());
        }
        private void changeSfxVolume(object sender, RangeBaseValueChangedEventArgs e)
        {
            sfxVolume = (sender as Slider).Value / 100;
            if (sfxPlayer != null)
                sfxPlayer.Volume = mainVolume * sfxVolume;
            if (narrator)
                speak((sfxVolume * 100).ToString());
        }
        private async void soundOverButton(object sender, PointerRoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\audios");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("overButton.wav");

            sfxPlayer.AutoPlay = false;
            sfxPlayer.Source = MediaSource.CreateFromStorageFile(file);

            sfxPlayer.Play();

            string tag;
            if ((sender as Button) != null)
                tag = (sender as Button).Tag.ToString();
            else if((sender as CheckBox) != null)
                tag = (sender as CheckBox).Tag.ToString();
            else if((sender as Slider) != null)
                tag = (sender as Slider).Tag.ToString();
            else if((sender as ComboBoxItem) != null)
                tag = (sender as ComboBoxItem).Tag.ToString();
            else
                tag = (sender as ComboBox).Tag.ToString();

            if (narrator)
                speak("Estàs sobre" + tag);
        }

        private async void soundClickButton(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\audios");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("clickButton.wav");

            sfxPlayer.AutoPlay = false;
            sfxPlayer.Source = MediaSource.CreateFromStorageFile(file);

            sfxPlayer.Play();

            string tag;
            if ((sender as Button) != null)
                tag = (sender as Button).Tag.ToString();
            else if ((sender as CheckBox) != null)
                tag = (sender as CheckBox).Tag.ToString();
            else
                tag = (sender as Slider).Tag.ToString();

            if (narrator)
                speak("Pulsaste" + tag);
        }
        private async void soundClickButton(object sender, object e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\audios");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("clickButton.wav");

            sfxPlayer.AutoPlay = false;
            sfxPlayer.Source = MediaSource.CreateFromStorageFile(file);

            sfxPlayer.Play();
            string tag;
            if ((sender as Button) != null)
                tag = (sender as Button).Tag.ToString();
            else if ((sender as CheckBox) != null)
                tag = (sender as CheckBox).Tag.ToString();
            else if ((sender as Slider) != null)
                tag = (sender as Slider).Tag.ToString();
            else if ((sender as ComboBoxItem) != null)
                tag = (sender as ComboBoxItem).Tag.ToString();
            else
                tag = (sender as ComboBox).Tag.ToString();

            if (narrator)
                speak("Pulsaste" + tag);
        }
        #endregion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        #region goBack
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
            soundClickButton(sender, e);
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
        #endregion

        #region FullScreen
        private void ToggleFullScreen_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                // The SizeChanged event will be raised when the exit from full-screen mode is complete.
                if (narrator)
                    speak("Modo Ventana");
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                    // The SizeChanged event will be raised when the entry to full-screen mode is complete.
                    if(narrator)
                        speak("Modo Pantalla Completa");
                }
            }
        }


        #endregion

        #region Language
        private void SetAppLanguage(object sender, object e)
        {
            int index = (sender as ComboBox).SelectedIndex;
            switch (index)
            {
                case 0:
                    ApplicationLanguages.PrimaryLanguageOverride = "es";
                    lang = 0;
                    break;
                case 1:
                    ApplicationLanguages.PrimaryLanguageOverride = "en";
                    lang = 1;
                    break;
                case 2:
                    ApplicationLanguages.PrimaryLanguageOverride = "zh-Hans";
                    
                    lang = 2;
                    break;
            }
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
            Frame.Navigate(this.GetType());

            soundClickButton(sender, e);
        }

        #endregion

        #region ColorBlindness
        private void SetColorMode(object sender, object e)
        {
            int index = (sender as ComboBox).SelectedIndex;
            switch (index)
            {
                case 0:
                    BG_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Stars.jpg"));
                    break;
                case 1:
                    BG_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Stars_Prot.png"));
                    break;
                case 2:
                    BG_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Stars_Deu.png"));
                    break;
                case 3:
                    BG_Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Stars_Tri.png"));
                    break;
            }

            soundClickButton(sender, e);
        }
        #endregion
        private void narratorClick(object sender, RoutedEventArgs e)
        {
            narrator = !narrator;
            if (narrator)
                speak("Narrador Activado");
        }
    }
}
