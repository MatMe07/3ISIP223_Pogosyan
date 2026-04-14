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

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для RecordAppoitmantWIndow.xaml
    /// </summary>
    public partial class RecordAppoitmantWIndow : Window
    {
        public RecordAppoitmantWIndow()
        {
            InitializeComponent();
        }

        void MessageBoxRecord()
        {
            var result = MessageBox.Show(
                "Вы записываетесь на:\n\n" +
                "• Маникюр (классический)\n" +
                "• Анна Кузнецова\n" +
                "• 15 апреля 2026, 10:00\n\n" +
                "Подтвердить запись?",
                "ПОДТВЕРДИТЕ ЗАПИСЬ",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
        }
    }
}
