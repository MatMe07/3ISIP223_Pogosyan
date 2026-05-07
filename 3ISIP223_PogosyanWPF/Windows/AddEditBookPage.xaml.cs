using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Drawing.Imaging;
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
            BitmapImage bi = null;
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image documents (.jpg)|*.jpg";
            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                var ind = dialog.FileName.LastIndexOf('\\')+1;
                var imgPath = dialog.FileName.Substring(ind);
                var p = Environment.CurrentDirectory;
                
                var path = $"{p}\\Images\\Covers\\{imgPath}";
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(dialog.FileName);
                bi.EndInit();

                //var bitImage = new BitmapImage( new Uri(dialog.FileName));
                ImageCover.Source = bi;
                //bitImage
                JpegBitmapEncoder jpg = new JpegBitmapEncoder();
                jpg.Frames.Add(BitmapFrame.Create(bi));

                using (Stream stm = File.Create(path))
                {
                    jpg.Save(stm);
                }


                //SaveFileDialog save = new SaveFileDialog();
                //save.Title = "Save picture as ";
                //save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                //if (bi != null)
                //{
                //    if (save.ShowDialog() == true)
                //    {
                        
                //        JpegBitmapEncoder jpg = new JpegBitmapEncoder();
                //        jpg.Frames.Add(BitmapFrame.Create(bi));
                //        using (Stream stm = File.Create(save.FileName))
                //        {
                //            jpg.Save(stm);
                //        }
                //    }
                //}

            }
        }
    }
}
