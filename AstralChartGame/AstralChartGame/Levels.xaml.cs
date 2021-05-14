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

            switch (code){
                case "Menu":{
                     Frame.Navigate(typeof(Menu));
                }
                break;
                case "Pause":{
                     Frame.Navigate(typeof(Pause));
                }
                break;
                default:
                    break;
            }
        }
    }
}
