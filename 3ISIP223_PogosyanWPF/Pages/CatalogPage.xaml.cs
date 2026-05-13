using _3ISIP223_PogosyanWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {

        public List<string> lst {  get; set; }
        public CatalogPage()
        {
            lst = new List<string> { "hello", "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" };
            //DataContext = this;
            InitializeComponent();
            //listB.ItemsSource = lst;
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
            wind.frameCatalog.NavigationService.Navigate(new BookPage("CatalogPage"));
            //var res = MessageBox.Show("Книга {bookId} → {status}");
            //if (res == MessageBoxResult.OK) listBooks.SelectedIndex = -1;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var wind = MainWindow.GetInstance();
            Book book = book = (sender as Border).Tag as Book;

            if ( book == null)
            {
                book = (sender as TextBlock).Tag as Book;
            }
            (DataContext as CatalogViewModel).SetSelectBook(book);
            wind.frameCatalog.NavigationService.Navigate(new BookPage("CatalogPage"));
        }
    }
}
