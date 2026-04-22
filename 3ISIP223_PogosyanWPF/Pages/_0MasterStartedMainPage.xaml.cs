using _3ISIP223_PogosyanWPF.ViewModels;
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
    /// Логика взаимодействия для _0MasterStartedMainPage.xaml
    /// </summary>
    public partial class _0MasterStartedMainPage : Page
    {
        private AppointmentsViewModel _viewModel;

        public _0MasterStartedMainPage()
        {
            InitializeComponent();
            _viewModel = (AppointmentsViewModel)DataContext;

            datePick.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _0StartedMainPage.GetMainPageFrame.NavigationService.GoBack();
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var appointment = button?.Tag as Appointment;

            if (appointment == null) return;

            if (WorkDataBase.Instance.CurrentUser == null)
            {
                MessageBox.Show("Для записи необходимо войти в аккаунт",
                    "Требуется авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (WorkDataBase.Instance.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Ваш аккаунт заморожен. Обратитесь к администратору.",
                    "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var wind = new RecordAppoitmantWIndow(appointment);
            var res = wind.ShowDialog();
            if (res==true)
                _0StartedMainPage.GetMainPageFrame.NavigationService.GoBack();

        }
    }
}