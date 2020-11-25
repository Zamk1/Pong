using System;
using System.Windows;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für StartDlg.xaml
    /// </summary>
    public partial class StartDlg : Window
    {
        public Double Radius { get; set; }
        public int SelectedColorShema;
        public double paddelsize;
        public int maxscore;

        public StartDlg()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Radius = Convert.ToDouble(radius.Text);
                paddelsize = Convert.ToDouble(paddel.Text);
                maxscore = Convert.ToInt32(Score.Text);

                if (Radius < 1 || Radius > 20)
                {
                    throw new Exception("Der Radius muss zwischen 0 und 21 liegen.");
                }else if (paddelsize < 50 || paddelsize > 180)
                {
                    throw new Exception("Die Paddel Size muss zwischen 0 und 181 liegen.");
                }
                else if (maxscore < 1 || maxscore > 100)
                {
                    throw new Exception("Der Radius muss zwischen 0 und 101 liegen.");
                }
                else
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_farben.Items.Add("Standart");
            cmb_farben.Items.Add("Black and Blue");
            cmb_farben.Items.Add("Dark Mode");
            cmb_farben.Items.Add("Erweitert");
            cmb_farben.SelectedIndex = 0;
        }

        private void cmb_farben_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedColorShema = cmb_farben.SelectedIndex;
            if (cmb_farben.SelectedIndex == 3)
            {
                Window.Width = 600;
                togglevisibilty(Visibility.Visible);
            }
            else
            {
                Window.Width = 320;
                togglevisibilty(Visibility.Hidden);
            }
            Console.WriteLine(SelectedColorShema);
        }

        private void togglevisibilty(Visibility x)
        {
            l1.Visibility = x;
            l2.Visibility = x;
            l3.Visibility = x;
            l4.Visibility = x;
            l5.Visibility = x;
            l6.Visibility = x;
            l7.Visibility = x;
            l8.Visibility = x;
            l9.Visibility = x;
            l10.Visibility = x;

            ClrPcker_Background.Visibility = x;
            ClrPcker_Ball.Visibility = x;
            ClrPcker_hud.Visibility = x;
            ClrPcker_Minz.Visibility = x;
            ClrPcker_Paddel.Visibility = x;
            ClrPcker_Sekz.Visibility = x;
            ClrPcker_Spielfeldrand.Visibility = x;
            ClrPcker_Stuz.Visibility = x;
            ClrPcker_Uhrfill.Visibility = x;
            ClrPcker_Uhrstroke.Visibility = x;
        }

        private void Check_paddel_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("check2");
            if (check_paddel.IsChecked.Value)
            {
                Player_two.Visibility = Visibility.Hidden;
                l_player_two.Visibility = Visibility.Hidden;
            }
            else
            {
                Player_two.Visibility = Visibility.Visible;
                l_player_two.Visibility = Visibility.Visible;
            }
        }
    }
}
