using System.Linq;
using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class AddEditUserWindow : Window
    {
        private AdminViewModel _viewModel;
        private User _editingUser;

        public AddEditUserWindow(AdminViewModel viewModel, User user = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _editingUser = user;

            cmbRole.ItemsSource = _viewModel.AllRoles.Select(s => s.Name) ;

            if (user != null)
            {
                TitleText.Text = "Редактирование пользователя";
                txtLastName.Text = user.LastName;
                txtFirstName.Text = user.FirstName;
                txtSecondName.Text = user.SecondName;
                txtPhone.Text = user.Phone;
                txtEmail.Text = user.Email;
                cmbRole.SelectedItem = user.TypeRole?.Name;
                txtPassword.Text = user.password;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingUser == null)
            {
                var user = new User
                {
                    LastName = txtLastName.Text,
                    FirstName = txtFirstName.Text,
                    SecondName = txtSecondName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Role_ID = cmbRole.SelectedIndex + 1,
                    IsFrozen = false,
                    password = txtPassword.Text
                };
                _viewModel.AddUser(user);
            }
            else
            {
                _editingUser.LastName = txtLastName.Text;
                _editingUser.FirstName = txtFirstName.Text;
                _editingUser.SecondName = txtSecondName.Text;
                _editingUser.Phone = txtPhone.Text;
                _editingUser.Email = txtEmail.Text;
                _editingUser.Role_ID = cmbRole.SelectedIndex + 1;
                _viewModel.UpdateUser(_editingUser);
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}