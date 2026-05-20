using _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.Pages.ProfilePages
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditPage.xaml
    /// </summary>
    public partial class ProfileEditPage : Page
    {
        public Frame FrameMainPage;
        public Action Action;
        private ProfileEditViewModel viewModel;
        public ProfileEditPage(Action action)
        {
            InitializeComponent();
            viewModel = DataContext as ProfileEditViewModel;
            Action = action;
        }



        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //FrameMainPage.NavigationService.Navigate(new ProfileMainPage());
            if (viewModel.IsEnabledButton)
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

                var result = await MainWindow.GetInstance().myHost.ShowDialog(dialogCont);

                if (result is true)
                {
                    //DialogResult = false;
                    //viewModel.Cancel();
                    //Close();
                    viewModel.CancelChanges();
                    Action();

                }
            }
            else
            {
                Action();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SaveChanges())
            {
                Action();

            }
        }
    }
}
