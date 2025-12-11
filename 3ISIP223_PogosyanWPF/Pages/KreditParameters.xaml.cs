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
    /// Логика взаимодействия для KreditParameters.xaml
    /// </summary>
    public partial class KreditParameters : Page
    {
        private Frame TotalFrame;
        public KreditParameters(Frame frame)
        {
            InitializeComponent();
            TotalFrame = frame;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (TotalFrame != null) Grid.SetColumnSpan(TotalFrame, 2);
        }

        private void textProcent_TextInput(object sender, TextCompositionEventArgs e)
        {
            //TextBox text = sender as TextBox;
            //SumKredit.Text = text.Text;
        }

        private void textProcent_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            
            SumKredit.Text = text.Text;
        }

        private void textProcent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);

        }
    }
}
