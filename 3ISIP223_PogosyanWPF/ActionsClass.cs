using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;

namespace _3ISIP223_PogosyanWPF
{
    public static class ActionsClass
    {
        public static void UpdateToReadingList(Book book, string status, bool updOrAdd, SnackbarMessageQueue MyMessageQueue)
        {
            var txt = new StackPanel() { Orientation = Orientation.Horizontal };
            txt.Children.Add(new PackIcon()
            {

                Kind = updOrAdd ? PackIconKind.SwapHorizontalBold : PackIconKind.CheckBold,
                Margin = new System.Windows.Thickness(0, 0, 7, 0),
                Width = 20,
                Height = 20,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#42A757")
            });
            if (updOrAdd)
            {
                txt.Children.Add(new TextBlock() { Text = $"\"{book.Title}\" перемещена в \"{status}\"" });
            }
            else
            {
                txt.Children.Add(new TextBlock() { Text = $"\"{book.Title}\" добавлена в \"{status}\"" });

            }

            MyMessageQueue.Enqueue(txt);
            MainWindow.GetInstance().MySnackbar.MessageQueue = MyMessageQueue;

        }

        public static void RemoveBookFromReadingList(Book book, SnackbarMessageQueue MyMessageQueue)
        {
            var txt = new StackPanel() { Orientation = Orientation.Horizontal };
            txt.Children.Add(new PackIcon()
            {

                Kind =  PackIconKind.CancelBold,
                Margin = new System.Windows.Thickness(0, 0, 7, 0),
                Width = 20,
                Height = 20,
                Foreground = Brushes.IndianRed
            });
            
            txt.Children.Add(new TextBlock() { Text = $"\"{book.Title}\" удалена из всех списков" });


            MyMessageQueue.Enqueue(txt);
            MainWindow.GetInstance().MySnackbar.MessageQueue = MyMessageQueue;

        }
    }
}
