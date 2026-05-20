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
    /// Логика взаимодействия для FrozenBooksPage.xaml
    /// </summary>
    public partial class FrozenBooksPage : Page
    {
        public List<string> lstRand { get; set; }

        public FrozenBooksPage()
        {
            //lstRand = new List<string> { "fdf", "sdf", "sdf", "sdf" };
            //DataContext = this;
            InitializeComponent();
        }

        private void btnChallengeBook_Click(object sender, RoutedEventArgs e)
        {
            Book book = (sender as Button).Tag as Book;
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new UnfreezeRequestsWindow("Book", book.BookId);
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
            if (res == true)
            {
                ActionsClass.SnackBarEnqueue(
                    text: $"Запрос на оспаривание отправлено!",
                    foregroundHEX: "#42A757",
                    iconKind: PackIconKind.CheckBold,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    main: true
                );
            }
        }
    }
}
