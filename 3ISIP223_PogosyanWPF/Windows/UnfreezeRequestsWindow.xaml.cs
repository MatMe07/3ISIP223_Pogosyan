using _3ISIP223_PogosyanWPF.ViewModels;
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
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для UnfreezeRequestsWindow.xaml
    /// </summary>
    public partial class UnfreezeRequestsWindow : Window
    {

        //public int LengthUnfreezeText { get; set; } = 0;
        public int? BookId = null;
        public UnfreezeRequestsWindow(string types, int? bookId = null)
        {
            //DataContext = this;
            InitializeComponent();
            BookId = bookId;
            (DataContext as UnfreezeRequestsViewModel).TypeRequest = types;
            if (types == "Book")
            {
                RequestIitle.Text = $"Объясните причину, почему стоит разморозить книгу";
            }
            else if (types == "Author")
            {
                RequestIitle.Text = $"Объясните причину, почему стоит разморозить аккаунт";
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as UnfreezeRequestsViewModel).SendRequest(BookId))
            {

                DialogResult = true;

            }
        }
    }
}
