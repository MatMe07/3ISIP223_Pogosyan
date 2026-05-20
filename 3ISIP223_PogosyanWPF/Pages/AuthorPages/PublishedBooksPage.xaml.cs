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

namespace _3ISIP223_PogosyanWPF.Pages.AuthorPages
{
    /// <summary>
    /// Логика взаимодействия для PublishedBooksPage.xaml
    /// </summary>
    public partial class PublishedBooksPage : Page
    {
        //public List<string> lstRand { get; set; }

        public PublishedBooksPage()
        {
            //lstRand = new List<string> { "fdf", "sdf", "sdf", "sdf", "sdf" };
            //DataContext = this;
            InitializeComponent();
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as PublishedBooksViewModel).CheckUserFreez())
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Ваш аккаунт заморожен! Обратитесь к администратору",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Block,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
                return;

            }

            Book book = (sender as Button).Tag as Book;
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new AddEditBookPage(true, book.BookId);
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
            if (res == true)
            {
                (DataContext as PublishedBooksViewModel).UpdateBooks();
                ActionsClass.SnackBarEnqueue(
                        text: "Книга успешно обновлена!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
            }
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as PublishedBooksViewModel).CheckUserFreez())
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Ваш аккаунт заморожен! Обратитесь к администратору",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Block,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
                return;

            }
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new AddEditBookPage(false);
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
            if (res == true)
            {
                (DataContext as PublishedBooksViewModel).UpdateBooks();
                ActionsClass.SnackBarEnqueue(
                    text: "Книга успешно сохранена!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
            }   
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as PublishedBooksViewModel).UpdateBooks();

        }
    }
}
