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

        public int LengthUnfreezeText { get; set; } = 0;
        public UnfreezeRequestsWindow(string types)
        {
            DataContext = this;
            InitializeComponent();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void richTextsUnfreeze_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(
                richTextsUnfreeze.Document.ContentStart,
                richTextsUnfreeze.Document.ContentEnd
            );
            LengthUnfreezeText = textRange.Text.Length;
        }
    }
}
