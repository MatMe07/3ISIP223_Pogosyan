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
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    /// <summary>
    /// Логика взаимодействия для DatailAppointment.xaml
    /// </summary>
    public partial class DatailAppointment : Window
    {
        private MasterAppointmentDetailsViewModel viewModel;
        public DatailAppointment(Appointment appointment)
        {
            InitializeComponent();
            DataContext = viewModel = new MasterAppointmentDetailsViewModel(appointment);
            viewModel.Appointment = appointment;

            if (appointment.Status == "Completed")
            {
                btnEompleted.IsEnabled = false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnOtmena_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
