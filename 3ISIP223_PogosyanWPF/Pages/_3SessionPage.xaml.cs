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
    /// Логика взаимодействия для _3SessionPage.xaml
    /// </summary>
    public partial class _3SessionPage : Page
    {
        public Brush ColorSelect = (Brush)(new BrushConverter().ConvertFrom("#FFBD257F"));
        public Brush ColorPlace = (Brush)(new BrushConverter().ConvertFrom("#FF4D82FF"));
        //public Session session {  get; set; }

        public List<Button> buttonsBusy = new List<Button>();

        public SessionDetailView sessionDetail {  get; set; }
        public _3SessionPage(Session selectSession)
        {
            InitializeComponent();
            //session = selectSession;
            sessionDetail = new SessionDetailView(selectSession);
            DataContext = sessionDetail;

            int row = sessionDetail.GetCountRow;
            int col = sessionDetail.GetCountCol;


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col+2; j++)
                {
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = new GridLength(1, GridUnitType.Auto);

                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = new GridLength (1, GridUnitType.Auto);

                    gridMesta.RowDefinitions.Add(rowDefinition);
                    gridMesta.ColumnDefinitions.Add(columnDefinition);
                    if(j != 0 && j != col + 1)
                    {

                        var seat = sessionDetail.Seats.FirstOrDefault(s=>s.RowNumber == (i+1) && s.SeatNumber == j);
                        Button btn = new Button();
                        btn.Content = j.ToString();
                        btn.Width = 30;
                        btn.Height = 30;
                        btn.Foreground = Brushes.White;
                        btn.BorderThickness = new Thickness (0);
                        btn.Margin = new Thickness(5);
                        if (seat.IsActive)
                        {
                            btn.Background = ColorPlace;
                            btn.Click += Btn_Click;
                        }
                        else
                        {
                            btn.Background = Brushes.Gray;
                            buttonsBusy.Add(btn);
                        }

                        //if ((j + i) % 2 == 0) {
                        //    btn.Background = Brushes.Gray;
                        //}



                        Grid.SetRow(btn, i);
                        Grid.SetColumn(btn, j);

                        gridMesta.Children.Add(btn);
                    }
                    else
                    {
                        TextBlock text = new TextBlock();
                        text.Text = $"{i + 1}";
                        if(j == col + 1) text.Margin = new Thickness(30, 5, 0, 5);
                        else
                            text.Margin = new Thickness(0, 5, 30, 5);
                        Grid.SetRow(text, i);
                        Grid.SetColumn(text, j);

                        gridMesta.Children.Add(text);

                    }
                }
            }
            //sessionDetail.UpdateParams();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            //txt.Text = btn.Background.ToString();
            switch (btn.Background.ToString())
            {
                case "#FF4D82FF": // место
                    {
                        sessionDetail.AddSeat(Grid.GetRow(btn)+1, Grid.GetColumn(btn));
                        btn.Background = ColorSelect;
                        break;
                    }
                case "#FFBD257F": // выбрать
                    {
                        btn.Background = ColorPlace;
                        sessionDetail.DeleteSeat(Grid.GetRow(btn) + 1, Grid.GetColumn(btn));
                        break;
                    }
            }
            UpdateButton();
            //sessionDetail.UpdateParams();

        }

        public void UpdateButton()
        {
            if (sessionDetail.SelectSeat.Count() > 0)
            {
                stackBtn.Visibility = Visibility.Visible;
            }
            else {
                stackBtn.Visibility = Visibility.Collapsed;
            } 
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true)
            {
                foreach(var btn in buttonsBusy)
                {
                    btn.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                foreach (var btn in buttonsBusy)
                {
                    btn.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _4MakingTicketPage(sessionDetail));
        }
    }
}
