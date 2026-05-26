using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels;
using _3ISIP223_PogosyanWPF.Windows;
using MaterialDesignThemes.Wpf;
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
        private MainWindow mainWindow;
        public string WhereFrom;

        private BookViewModel viewModel;

        public BookPage(string whereFrom)
        {
            //DataContext = this;
            WhereFrom = whereFrom;
            InitializeComponent();
            mainWindow = MainWindow.GetInstance();
            viewModel = DataContext as BookViewModel;
        }


        private void ReviewListItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            Console.WriteLine("Click");

            if (menuItem.IsChecked == true)
            {
                string status = menuItem.Header.ToString();

                var SelectReview = menuItem.Tag as Review;
                
                viewModel.ShowMessage(SelectReview);

                menuItem.IsChecked = false;
            }
            //int bookId = (int)menuItem.Tag;

             (menuItem.Parent as ContextMenu).IsOpen = false;
        }



        private void btnReviewPoints_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CheckUserFreez())
            {
                return;
            }
            var button = sender as Button;
            var SelectReview = button.Tag as Review;

            var contextMenu = FindResource("ReviewListMenu") as ContextMenu;

            contextMenu.DataContext = viewModel;
            contextMenu.PlacementTarget = button;
            contextMenu.IsOpen = true;
            foreach (MenuItem item in contextMenu.Items)
            {
                //item.Click -= ReadingListItem_Click;
                item.Click -= ReviewListItem_Click;
                item.Click += ReviewListItem_Click;
                item.Tag = SelectReview;
            }
        }

        private void BtnAddList_Click(object sender, RoutedEventArgs e)
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
                Console.WriteLine($"{item.Header.ToString()} - {isChecked}");
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
            if (viewModel.CheckUserFreez())
            {
                return;

            }
            if (viewModel.CheckHaveReview() == true)
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Вы уже оставляли отзыв на эту книгу. Можно оставить только один отзыв",
                    foregroundHEX: "#FFFF9800",
                    iconKind: PackIconKind.Alert,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    main: true
                );
                return;
            }
            mainWindow.BlurAdd(true);
            var wind = new AddReviewWindow();
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
            if (res == true) { 
                viewModel.LoadReviews(); 
                ActionsClass.SnackBarEnqueue(
                text: "Спасибо! Ваш отзыв успешно добавлен",
                foregroundHEX: "#FF04BE5A",
                iconKind: PackIconKind.CheckCircle,
                MyMessageQueue: new SnackbarMessageQueue(),
                main: true
            );
            }
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

            var book = (sender as Button).Tag as Book;

            viewModel.ShowMessage(SelBook: book);

        }

        private void btnComplainUser_Click(object sender, RoutedEventArgs e)
        {

            var author = ((sender as Button).Tag as Book).User;

            viewModel.ShowMessage(Author:author);

        }

        private void FreezeAuthor_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowMessage(Author: ((sender as Button).Tag as Book).User);
        }

    }
}
