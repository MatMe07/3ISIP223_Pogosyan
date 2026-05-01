using _3ISIP223_PogosyanWPF.Windows;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var wind = new LoginRegisterWindows();
            //var res = wind.ShowDialog();
            //if (res != false) this.Close();





            InitializeComponent();
        }
        public static MainWindow GetInstance()
        {
            return Application.Current.MainWindow as MainWindow;
        }
        public void BlurAdd(bool IsBlur)
        {
            if (IsBlur)
            {
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 10;
                this.Effect = blurEffect;
            }
            else
            {
                this.Effect = null;
            }
        }
    }
}
