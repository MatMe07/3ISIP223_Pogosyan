using _3ISIP223_PogosyanWPF.ViewModels;
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
    /// Логика взаимодействия для FreezeRequestPage.xaml
    /// </summary>
    public partial class FreezeRequestPage : Window
    {
        private Object Items;
        private bool IsAdmin;
        public FreezeRequestPage(string types, object items, bool isAdmin = false)
        {
            InitializeComponent();

            (DataContext as FreezeRequestViewModel).ChangeTitle(types, isAdmin);
            Items = items;
            IsAdmin = isAdmin;
            //LoadTitles(types);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

            
        //public void LoadTitles(string types)
        //{

        //}

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as FreezeRequestViewModel).SaveRequest(Items, IsAdmin))
            {

                DialogResult = true;
                this.Close();

            }

        }
    }
}
