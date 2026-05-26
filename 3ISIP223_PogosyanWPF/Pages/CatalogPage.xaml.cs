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

        //public List<string> lst {  get; set; }
        private CatalogViewModel viewModel;
        private MainWindow MWind;
        public CatalogPage()
        {
            //lst = new List<string> { "hello", "world" , "world" , "world" , "world" , "world" , "world" , "world" , "world" };
            //DataContext = this;
            InitializeComponent();
            viewModel = DataContext as CatalogViewModel;
            MWind = MainWindow.GetInstance();
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
                item.Click -= ReadingListItem_Click;

                var isChecked = viewModel.CheckReadigItemToList(book, item.Header.ToString());
                item.IsChecked = isChecked;

                item.Click += ReadingListItem_Click;

                item.Tag = contextMenu;
            }
        }
        private void ReadingListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuitem = sender as MenuItem;
            var contextMenu = menuitem.Tag as ContextMenu;
            var book = contextMenu.Tag as Book;
            string clickedStatus = menuitem.Header.ToString();

            bool wasChecked = menuitem.IsChecked;

            menuitem.IsChecked = !wasChecked;

            if (wasChecked)
            {
                foreach (MenuItem item in contextMenu.Items)
                {
                    if (item.IsCheckable && item != menuitem && item.IsChecked)
                    {
                        item.IsChecked = false;
                    }
                }

                menuitem.IsChecked = true;
                viewModel.UpdateToReadingList(book, clickedStatus);
            }
            else
            {
                menuitem.IsChecked = false;
                viewModel.RemoveBookFromReadingList(book);
            }

            contextMenu.IsOpen = false;
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Book book = (sender as Border)?.Tag as Book;

            if ( book == null)
            {
                book = (sender as TextBlock).Tag as Book;
            }
            viewModel.SetSelectBook(book);
            MWind.frameCatalog.NavigationService.Navigate(new BookPage("CatalogPage"));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.UpdBooks();
        }
    }
}
