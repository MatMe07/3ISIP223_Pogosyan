using _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
     
        public AddEditViewModel viewModel;

        public AddEditBookPage(bool IsEdit, int? bookId = null)
        {

            InitializeComponent();
            viewModel = DataContext as AddEditViewModel;
            LoadData(IsEdit);
            viewModel.LoadData(IsEdit, bookId);
        }

        public void LoadData(bool IsEdit)
        {
            if (IsEdit)
            {
                textTitle.Text = "Редактировать произведение";
                btnSend.Content = "Обновить";
            }
            else
            {
                textTitle.Text = "Новое произведение";
                btnSend.Content = "Создать";

            }
        }

        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.HasChanges())
            {
                var dialogCont = new StackPanel
                {
                    Margin = new Thickness(20),
                    MinWidth = 250,
                    Children =
                {
                    new TextBlock
                    {
                        Text = "Отменить изменения?",
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(0, 0, 0, 15)
                    },
                    new TextBlock
                    {
                        Text = "Все несохранённые изменения будут потеряны.",
                        Margin = new Thickness(0, 0, 0, 20)
                    },
                    new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Children =
                        {
                            new Button
                            {
                                Content = "Нет",
                                Margin = new Thickness(0, 0, 10, 0),
                                Command = DialogHost.CloseDialogCommand,
                                CommandParameter = false
                            },
                            new Button
                            {
                                Content = "Да",
                                Style = (Style) FindResource("MaterialDesignFlatButton"),
                                Command = DialogHost.CloseDialogCommand,
                                CommandParameter = true
                            }
    }
                    }
                }
                };

                var result = await myHost.ShowDialog(dialogCont);

                if (result is true)
                {
                    DialogResult = false;
                    //viewModel.Cancel();
                    //Close();
                }

            }
            else
            {
                Close();
            }
            //DialogResult = false;
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
            viewModel.AttachFile();

        }

        private void btnLoadCover_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AttachCover();


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.SelectedIndex = -1;
            Keyboard.ClearFocus();
        }

        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            var ch = sender as MaterialDesignThemes.Wpf.Chip;
            var bg = ch.DataContext as BookGenre;
            //lst.Remove(ch.Content.ToString());
            viewModel.RemoveGenre(bg.Genre);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.CancelData();
            DialogResult = false;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SaveBook())
            {
                DialogResult = true;

            }
        }
    }
}
