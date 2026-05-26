using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;

namespace _3ISIP223_PogosyanWPF
{
    public static class ActionsClass
    {

        public static void SnackBarEnqueue( string text, string foregroundHEX, PackIconKind iconKind,SnackbarMessageQueue MyMessageQueue, bool main = false)
        {
            var txt = new StackPanel() { Orientation = Orientation.Horizontal };
            txt.Children.Add(new PackIcon()
            {

                Kind = iconKind,
                Margin = new Thickness(0, 0, 7, 0),
                Width = 20,
                Height = 20,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(foregroundHEX)
            });

            txt.Children.Add(new TextBlock() { Text = text,
                TextWrapping = TextWrapping.Wrap,

            });


            MyMessageQueue.Enqueue(txt);
            if (main) 
                MainWindow.GetInstance().MySnackbar.MessageQueue = MyMessageQueue;
        }
        public static void SnackBarEnqueue( string text,string buttonText, Action onButtonClick, string foregroundHEX, PackIconKind iconKind,SnackbarMessageQueue MyMessageQueue, bool main = false)
        {
            var txt = new StackPanel() { Orientation = Orientation.Horizontal };
            txt.Children.Add(new PackIcon()
            {

                Kind = iconKind,
                Margin = new Thickness(0, 0, 7, 0),
                Width = 20,
                Height = 20,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(foregroundHEX)
            });

            txt.Children.Add(new TextBlock
            {
                Text = text,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 10, 0)
            });

            MyMessageQueue.Enqueue(txt, buttonText, () => onButtonClick());
            //MyMessageQueue.
            if (main) 
                MainWindow.GetInstance().MySnackbar.MessageQueue = MyMessageQueue;
        }

    }
}
