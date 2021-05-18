using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AstralChartGame.Stages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Lev1 : Page
    {
        private Button A = null, B = null;
        public Lev1()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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

        private void createLine()
        {

        }

        private void select_level(object sender, RoutedEventArgs e)
        {
            if (A == null)
            {
                A = (Button)sender;
            }
            else if (B == null)
            {
                B = (Button)sender;
            }

            if (A != null && B != null)
            {
                // Create a Line  
                Line redLine = new Line();

                redLine.X1 = Level.Margin.Left + A.Margin.Left - (A.ActualWidth / 2);
                redLine.Y1 = Level.Margin.Top + A.Margin.Top + (A.ActualHeight / 2);
                redLine.X2 = Level.Margin.Left + B.Margin.Left - (B.ActualWidth / 2);
                redLine.Y2 = Level.Margin.Top + B.Margin.Top + (B.ActualHeight / 2);

                // Create a red Brush  
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.White;

                // Set Line's width and color  
                redLine.StrokeThickness = 4;
                redLine.Stroke = redBrush;

                // Add line to the Grid.  
                RelativePanel panel = father.Child as RelativePanel;

                panel.Children.Add(redLine);
                A = B = null;
            }
        }
    }
}
