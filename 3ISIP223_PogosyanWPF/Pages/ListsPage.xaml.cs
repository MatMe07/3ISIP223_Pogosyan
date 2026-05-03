using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListsPage.xaml
    /// </summary>
    public partial class ListsPage : Page
    {

        public ObservableCollection<string> lst { get; set; }

        public ListsPage()
        {
            lst = new ObservableCollection<string> { "hello", "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" };
            DataContext = this;
            InitializeComponent();
        }

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            //var bookId = (int)button.Tag;

            var contextMenu = FindResource("ReadingListMenu") as ContextMenu;
            contextMenu.PlacementTarget = button;
            contextMenu.IsOpen = true;
            foreach (MenuItem item in contextMenu.Items)
            {
                //item.Click -= ReadingListItem_Click;
                item.Click += ReadingListItem_Click;
                //item.Tag = bookId;
            }
        }
        private void ReadingListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            //int bookId = (int)menuItem.Tag;
            string status = menuItem.Header.ToString();

            //AddBookToReadingList(bookId, status);

            (menuItem.Parent as ContextMenu).IsOpen = false;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wind = MainWindow.GetInstance();
            wind.frameListBook.NavigationService.Navigate(new BookPage("ListPage"));
            listBooks.SelectedIndex = -1;
            //var res = MessageBox.Show("Книга {bookId} → {status}");
            //if (res == MessageBoxResult.OK) listBooks.SelectedIndex = -1;

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectTabItem = (sender as TabControl).SelectedItem as TabItem;
            //if ()
            lst.Clear();

            switch (selectTabItem.Tag.ToString())
            {
                case "Все книги":
                    {
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        break;
                    }
                case "Заброшено":
                    {
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        break;
                    }
                case "В планах":
                    {
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        break;
                    }
                case "Читаю":
                    {
                        lst.Add("1");
                        lst.Add("1");
                        lst.Add("1");
                        break;
                    }
                case "Прочитано":
                    {
                        lst.Add("1");
                        break;
                    }
            }
        }
    }
}
