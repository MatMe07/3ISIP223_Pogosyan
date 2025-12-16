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
    /// Логика взаимодействия для ContactDetailsAndSummary.xaml
    /// </summary>
    public partial class ContactDetailsAndSummary : Page
    {
        private bool changeValue { get; set; } = false;


        public  bool  bntBack = false;
        public ContactDetailsAndSummary()
        {
            InitializeComponent();
            //bntBack = bnt;
        }

        private void textTelephon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]) || ( ((TextBox)sender).Text + e.Text).Length > 11;
        }

        private void textName_TextChanged(object sender, TextChangedEventArgs e)
        {

            bntBack = ((TextBox)sender).Text.Length > 0;


            UpdateButton();
        }

        private void textTelephon_TextChanged(object sender, TextChangedEventArgs e)
        {
            bntBack = ((TextBox)sender).Text.Length > 0;

            UpdateButton();
        }

        private void UpdateButton()
        {
            bool emailZnak = textEmail.Text.Contains("@");
            bool lenTelephon = textTelephon.Text.Length == 11;
            bool nameLen = textName.Text.Length > 1;
            btnSend.IsEnabled = ( emailZnak  ) && ( lenTelephon  ) && ( nameLen  );
            bntBack = false;
        }


        private void textEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox textBox = sender as TextBox ;

            bntBack = ((TextBox)sender).Text.Length > 0;

            UpdateButton();

        }

    }
}
