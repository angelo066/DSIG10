using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Input;
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
    public sealed partial class GameState : Page
    {
        PointerPoint ptrPt;
        int Sel = -1;
        bool BotDer = false, BotIzq = false;

        CoreCursor pin;
        CoreCursor normal;

        public GameState()
        {
            this.InitializeComponent();
        }

        private void ImagenC_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ptrPt = e.GetCurrentPoint(MiCanvas);
            if (ptrPt.Properties.IsLeftButtonPressed) BotIzq = true;
            //Establecer Cursor
            if (ptrPt.Properties.IsRightButtonPressed) BotDer = true;
        }

        private void ImagenC_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint NewptrPt = e.GetCurrentPoint(MiCanvas);
            if ((Sel >= 0) && ((BotIzq || BotDer)))
                Window.Current.CoreWindow.PointerCursor = pin;
            {
                if (BotIzq)
                {
                    CompositeTransform m = (CompositeTransform)timon.RenderTransform;

                    int angulo = (int)NewptrPt.Position.X - (int)ptrPt.Position.X;

                    m.Rotation = angulo;
                }
            }
        }
        private void BackBut_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                this.Frame.Navigate(typeof(Pause));
            }
        }
        private void Catalejo_Pressed(object sender, KeyRoutedEventArgs e)
        {
            if (BotIzq)
                this.Frame.Navigate(typeof(Catalejo));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
