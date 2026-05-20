using MaterialDesignThemes.Wpf;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MaterialDesignThemes.Wpf.Theme;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace _3ISIP223_PogosyanWPF.Windows
{
    public partial class LoginRegisterWindows : Window
    {

        private ViewModels.LoginRegisterViewModel viewModel;

        public LoginRegisterWindows()
        {
            InitializeComponent();
            viewModel = DataContext as ViewModels.LoginRegisterViewModel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.LoginToAccc(viewModel.Login, passWordLog.Password.ToString()))
            {
                DialogResult = true;
                Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Register(viewModel.RegLogin, viewModel.RegDisplayName,
                viewModel.RegEmail,  passWordReg.Password.ToString(), passWordRegRep.Password.ToString()))
            {
                viewModel.RegLogin = null;
                viewModel.RegDisplayName = null;
                viewModel.RegEmail = null;
                viewModel.RegPassword = null;
                viewModel.RegConfirmPassword = null;
                passWordReg.Password = null;
                passWordRegRep.Password = null;

                tabControlMain.SelectedItem = item1;
            }
        }

    }
}