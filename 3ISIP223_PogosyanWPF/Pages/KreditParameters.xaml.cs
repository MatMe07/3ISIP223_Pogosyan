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
using static System.Net.Mime.MediaTypeNames;

namespace _3ISIP223_PogosyanWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для KreditParameters.xaml
    /// </summary>
    public partial class KreditParameters : Page
    {
        private Frame TotalFrame;
        double p = 0;
        double r = 12;
        int n = 12;
        public KreditParameters(Frame frame)
        {
            InitializeComponent();
            TotalFrame = frame;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (TotalFrame != null) Grid.SetColumnSpan(TotalFrame, 2);
        }


        private void textProcent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !char.IsDigit(e.Text[0]) || (((TextBox)sender).Text + e.Text).Length > 2;

        }

        private void changeParam()
        {
            double s = CarConfig.Instance.Car.CalculateTotalPrice() - p;
            double i = (r / 100) / 12;
            double A = s * (i *   Math.Pow(1+i, n)    /    Math.Pow(1 + i, n-1)    );

            SumKredit.Text = $"Cумма кредита: {s} ₽";  
            SumVznos.Text = $"Cумма первоначального взноса: {p} ₽";
            MonthPrice.Text = $"Ежемесячный платёж: {A} ₽";
            CarConfig.Instance.Car.MonthPrice = A;
            CarConfig.Instance.Car.SrokKredit = n;


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CountMounth == null) return;


            n = (int)e.NewValue;
            changeParam();
            CountMounth.Text = e.NewValue.ToString();



        }


        private void textProcent_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (double.TryParse(textBox.Text, out double znach))
            {
                p = CarConfig.Instance.Car.CalculateTotalPrice() * ( znach / 100);

            }
            else
            {
                p = CarConfig.Instance.Car.CalculateTotalPrice();

            }
                changeParam();

        }
    }
}
