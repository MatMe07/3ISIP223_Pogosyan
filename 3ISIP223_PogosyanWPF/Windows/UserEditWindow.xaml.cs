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
    /// Логика взаимодействия для UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        private UserEditViewModel viewModel;
        public UserEditWindow(User CurUser)
        {

            InitializeComponent();
            viewModel = (DataContext as UserEditViewModel);
            viewModel.User = CurUser;
        }

        private async void btnCancel_Click(object sender, RoutedEventArgs e)
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
                    viewModel.Cancel();
                    //Close();
                }

            }
            else
            {
                Close();
            }
            //else
            //{
            //    viewModel.Save();
            //}

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            DialogResult = true;
        }

        private void btnFreeze_Click(object sender, RoutedEventArgs e)
        {
            var but = sender as Button;
            if (but.Content.ToString() == "Заморозить")
            {
                viewModel.ShowMessage();
            }
            else
            {
                viewModel.UnfreezeUser();

                //myHost.Show("Отменить изменения?");
                //var result = MessageBox.Show("Отменить изменения?", "Подтверждение",
                //    MessageBoxButton.YesNo, MessageBoxImage.Question);
                //if (result == MessageBoxResult.Yes)
                //{

                //}
            }
        }
    }
}
