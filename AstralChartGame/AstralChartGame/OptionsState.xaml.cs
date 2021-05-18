using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI.ViewManagement;
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
    public sealed partial class OptionsState : Page
    {
        public OptionsState()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
            Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            //Check Options states
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
                FullScreen_CheckBox.IsChecked = true;
            else
                FullScreen_CheckBox.IsChecked = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        #region goBack
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
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
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                    // The SizeChanged event will be raised when the entry to full-screen mode is complete.
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
                    ApplicationLanguages.PrimaryLanguageOverride = "fr";
                    break;
                case 1:
                    ApplicationLanguages.PrimaryLanguageOverride = "fr";
                    break;
                case 2:
                    ApplicationLanguages.PrimaryLanguageOverride = "fr";
                    break;
            }
            this.Frame.Navigate(this.GetType());
        }
        #endregion
    }
}
