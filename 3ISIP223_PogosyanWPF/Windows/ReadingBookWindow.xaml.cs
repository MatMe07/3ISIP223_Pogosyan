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
    /// Логика взаимодействия для ReadingBookWindow.xaml
    /// </summary>
    public partial class ReadingBookWindow : Window
    {
        public ReadingBookWindow()
        {
            InitializeComponent();
            //LoadText();
        }

        private void LoadText()
        {
            string text = @"Евгений Онегин

См. тест «Евгений Онегин»
Роман в стихах

Pétri de vanité il avait encore plus de cette espèce d’orgueil qui fait avouer avec la même indifférence les bonnes comme les mauvaises actions, suite d’un sentiment de supériorité, peut-être imaginaire.
Tiré d’une lettre particulière[1]

Не мысля гордый свет забавить,
Вниманье дружбы возлюбя,
Хотел бы я тебе представить
Залог достойнее тебя,
Достойнее души прекрасной,
Святой исполненной мечты,
Поэзии живой и ясной,
Высоких дум и простоты;
Но так и быть – рукой пристрастной
Прими собранье пестрых глав,
Полусмешных, полупечальных,
Простонародных, идеальных,
Небрежный плод моих забав,
Бессонниц, легких вдохновений,
Незрелых и увядших лет,
Ума холодных наблюдений
И сердца горестных замет.

Глава первая

И жить торопится, и чувствовать спешит.
Князь Вяземский[2]

I

«Мой дядя самых честных правил,
Когда не в шутку занемог,
Он уважать себя заставил
И лучше выдумать не мог.
Его пример другим наука;
Но, Боже мой, какая скука
С больным сидеть и день и ночь,
Не отходя ни шагу прочь!
Какое низкое коварство
Полуживого забавлять,
Ему подушки поправлять,
Печально подносить лекарство,";

            TextContent.Text = text;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
