using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditBookPage.xaml
    /// </summary>
    public partial class AddEditBookPage : Window
    {
        public AddEditBookPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                btnIconMaxim.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;
            }
            else
            {
                WindowState = WindowState.Normal;
                btnIconMaxim.Kind = MaterialDesignThemes.Wpf.PackIconKind.Fullscreen;

            }

        }


        private void btnAttachFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                var ind = dialog.FileName.LastIndexOf('\\')+1;
                txtFileAttach.Text = dialog.FileName.Substring(ind);
                //Paragraph paragraph = new Paragraph();
                TextRange range;
                FileStream fileStream;
                if (File.Exists(dialog.FileName))
                {
                    range = new TextRange(ContentFile.Document.ContentStart, ContentFile.Document.ContentEnd);
                    fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    range.Load(fileStream, DataFormats.Text);
                    fileStream.Close();
                }



            }
        }
    }
}
