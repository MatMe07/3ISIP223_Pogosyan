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
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace _3ISIP223_PogosyanWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {

        public List<string> lst {  get; set; }
        private CatalogViewModel viewModel;
        public CatalogPage()
        {
            lst = new List<string> { "hello", "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" };
            //DataContext = this;
            InitializeComponent();
            viewModel = DataContext as CatalogViewModel;
            //listB.ItemsSource = lst;
        }

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var book = button.Tag as Book;

            var contextMenu = FindResource("ReadingListMenu") as ContextMenu;
            contextMenu.DataContext = viewModel;

            contextMenu.PlacementTarget = button;
            contextMenu.Tag = book;
            contextMenu.IsOpen = true;
            foreach (MenuItem item in contextMenu.Items)
            {
                //item.Click -= ReadingListItem_Click;
                //item.Click -= ReadingListItem_Click;
                item.Checked -= ReadingListItem_Checked;
                //item.Unchecked -= ReadingListItem_Unchecked;

                var stat = viewModel.CheckReadigItemToList(book, item.Header.ToString());
                Console.WriteLine($"{item.Header} - {stat}");

                item.IsChecked = stat;


                item.Checked += ReadingListItem_Checked;
                //item.Unchecked += ReadingListItem_Unchecked;

                //item.Click += ReadingListItem_Click;
                item.Tag = contextMenu;
            }
        }

        private void ReadingListItem_Checked(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem.Tag as ContextMenu;
            var book = contextMenu.Tag as Book;
            string status = menuItem.Header.ToString();
            Console.WriteLine($"CHECKED: {menuItem.Header} - {menuItem.IsChecked}");


            foreach (MenuItem item in contextMenu.Items)
            {
                if (item == menuItem && item.IsChecked)
                {
                    item.IsChecked = false;
                    viewModel.RemoveBookFromReadingList(book);
                }
                else if (item != menuItem && item.IsCheckable && item.IsChecked)
                {
                    item.IsChecked = false;
                    //viewModel.RemoveBookFromReadingList(book);
                }
            }

            //viewModel.AddBookToReadingList(book, status);
            viewModel.UpdateToReadingList(book, status);

            contextMenu.IsOpen = false;
        }

        private void ReadingListItem_Unchecked(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem.Tag as ContextMenu;
            var book = contextMenu.Tag as Book;
            Console.WriteLine($"UNCHECKED: {menuItem.Header} - {menuItem.IsChecked}");

            //viewModel.RemoveBookFromReadingList(book);
            contextMenu.IsOpen = false;
        }



        private void ReadingListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem.Tag as ContextMenu;
            var book = contextMenu.Tag as Book;
            string status = menuItem.Header.ToString();

            if (menuItem.IsChecked)
            {
                // Уже выбран - удаляем книгу из списков
                menuItem.IsChecked = false;
                viewModel.RemoveBookFromReadingList(book);
                Console.WriteLine($"Книга удалена из списка чтения");
            }
            else
            {
                // Не выбран - добавляем/обновляем статус
                foreach (MenuItem item in contextMenu.Items)
                {
                    if (item.IsCheckable && item != menuItem && item.IsChecked)
                    {
                        item.IsChecked = false;
                    }
                }
                menuItem.IsChecked = true;
                viewModel.UpdateToReadingList(book, status);
                Console.WriteLine($"Книге присвоен статус: {status}");
            }
            //(menuItem.Parent as ContextMenu).IsOpen = false;
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
            Book book = (sender as Border).Tag as Book;

            if ( book == null)
            {
                book = (sender as TextBlock).Tag as Book;
            }
            viewModel.SetSelectBook(book);
            wind.frameCatalog.NavigationService.Navigate(new BookPage("CatalogPage"));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.UpdBooks();
        }
    }
}
