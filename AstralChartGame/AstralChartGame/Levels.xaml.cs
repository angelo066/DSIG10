using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class Levels : Page
    {
        public string comingFrom;
        public Levels() {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e){
            var comingFrom = e.Parameter as string;
            backButton.Tag = comingFrom;
        }

        private void back_Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            string code = button.Tag.ToString();

            Type n = GetTypeByString(code, this.GetType().GetTypeInfo().Assembly);
            Frame.Navigate(n);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
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

        private void onLevelSelecte_click(object sender, RoutedEventArgs e){
            Button button = (Button)sender;
            string code = button.Tag.ToString();

            int stage = Int32.Parse(code);
            if (stage <= 6 && stage > 0){
                string level = "Lev" + code;
                //Frame.Navigate(typeof(Pause));
                Type n = GetTypeByString(level, this.GetType().GetTypeInfo().Assembly);
                Frame.Navigate(n);
            }
        }

        public static Type GetTypeByString(string type, Assembly lookIn)
        {
            var types = lookIn.DefinedTypes.Where(t => t.Name == type && t.IsSubclassOf(typeof(Windows.UI.Xaml.Controls.Page)));
            if (types.Count() == 0)
            {
                throw new ArgumentException("The type you were looking for was not found", "type");
            }
            else if (types.Count() > 1)
            {
                throw new ArgumentException("The type you were looking for was found multiple times.", "type");
            }
            return types.First().AsType();
        }
    }
}
