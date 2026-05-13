using _3ISIP223_PogosyanWPF.Windows;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public List<string> lstRand {  get; set; }
        private MainWindow mainWindow;
        public string WhereFrom;

        public BookPage(string whereFrom)
        {
            lstRand = new List<string> { "fdf", "sdf", "sdf", "sdf"};
            //DataContext = this;
            WhereFrom = whereFrom;
            InitializeComponent();
            mainWindow = MainWindow.GetInstance();
        }


        private void ReviewListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            //int bookId = (int)menuItem.Tag;
            string status = menuItem.Header.ToString();

            //AddBookToReadingList(bookId, status);

            (menuItem.Parent as ContextMenu).IsOpen = false;
        }


        private void ReadingListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            //int bookId = (int)menuItem.Tag;
            string status = menuItem.Header.ToString();

            //AddBookToReadingList(bookId, status);

            (menuItem.Parent as ContextMenu).IsOpen = false;
        }

        private void btnReviewPoints_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            //var bookId = (int)button.Tag;

            var contextMenu = FindResource("ReviewListMenu") as ContextMenu;
            contextMenu.PlacementTarget = button;
            contextMenu.IsOpen = true;
            foreach (MenuItem item in contextMenu.Items)
            {
                //item.Click -= ReadingListItem_Click;
                item.Click += ReviewListItem_Click;
            }
        }

        private void BtnAddList_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            //var bookId = (int)button.Tag;

            var cont = FindResource("ReadingListMenu") as ContextMenu;
            cont.PlacementTarget = button;
            cont.IsOpen = true;
            foreach (MenuItem item in cont.Items)
            {
                //item.Click -= ReadingListItem_Click;
                item.Click += ReadingListItem_Click;
                //item.Tag = bookId;
            }
        }

        private void btnOpenReading_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.BlurAdd(true);
            var wind = new ReadingBookWindow();
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.BlurAdd(true);
            var wind = new AddReviewWindow();
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (WhereFrom == "ListPage")
            {
                mainWindow.frameListBook.NavigationService.GoBack();

            }
            else if (WhereFrom == "CatalogPage")
                mainWindow.frameCatalog.NavigationService.GoBack();
        }

        private void btnComplainBook_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new FreezeRequestPage("Book");
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }

        private void btnComplainUser_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new FreezeRequestPage("Account");
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }
    }
}
