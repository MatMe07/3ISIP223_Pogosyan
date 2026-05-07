using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
//using static MaterialDesignThemes.Wpf.Theme;

namespace _3ISIP223_PogosyanWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditBookPage.xaml
    /// </summary>
    public partial class AddEditBookPage : Window
    {
     
        public ObservableCollection<string> lst { get; set; }
        public ObservableCollection<string> AllLst { get; set; }
            
        private BitmapImage bi = null;
        private string PathIm = "";

        public AddEditBookPage(bool IsEdit)
        {
            lst = new ObservableCollection<string>();
            AllLst = new ObservableCollection<string> { "Трагедия", "Комедия ", "Детектив", "Фантастика", "Поэма", "Элегия"};
            DataContext = this;
            InitializeComponent();
            LoadData(IsEdit);
        }

        public void LoadData(bool IsEdit)
        {
            if (IsEdit)
            {
                textTitle.Text = "Редактировать произведение";

            }
            else
            {
                textTitle.Text = "Новое произведение";

            }
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
            int maxChars = 500;
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

                    if (range.Text.Length > maxChars)
                    {
                        range.Text = range.Text.Substring(0, maxChars) + "...";
                    }

                }



            }
        }

        private void btnLoadCover_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image documents (.jpg)|*.jpg";
            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                var ind = dialog.FileName.LastIndexOf('\\')+1;
                var imgPath = dialog.FileName.Substring(ind);
                var p = Environment.CurrentDirectory;
                var firstInd = p.Substring(0, p.LastIndexOf("\\"));
                var df = firstInd.Substring(0, firstInd.LastIndexOf("\\"));

                PathIm = $"{df}\\Images\\Covers\\{imgPath}";

                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(dialog.FileName);
                bi.EndInit();

                ImageCover.Source = bi;

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;  
            var selectItm = comboBox.SelectedItem?.ToString();
            if (comboBox.SelectedItem != null)
            {
                if(lst.FirstOrDefault(s => s == selectItm) == null)
                    lst.Add(selectItm);


                comboBox.SelectedIndex = -1;
                Keyboard.ClearFocus();
            }
        }

        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            var ch = sender as MaterialDesignThemes.Wpf.Chip;
            lst.Remove(ch.Content.ToString());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            JpegBitmapEncoder jpg = new JpegBitmapEncoder();
            jpg.Frames.Add(BitmapFrame.Create(bi));

            using (Stream stm = File.Create(PathIm))
            {
                jpg.Save(stm);
            }

            DialogResult = true;
        }
    }
}
