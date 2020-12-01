using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartDlg dlg;
        Ball ball;
        DispatcherTimer timer;
        Double ticks_old;
        Uhr Timer;
        Line l;
        Paddel player1, player2;
        double strich, strich1;
        double scaling;
        Brush paddle, rectrand, uhrfill, uhrstroke, ballfill, hud, sekz, minz, stuz, background;
        BrushConverter brush = new BrushConverter();
        Brush darkmode;
        int score1 = 0;
        int score2 = 0;
        bool autopaddel = false;

        public MainWindow()
        {
            darkmode = (Brush)brush.ConvertFromString("#AFAFAF");
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }

        }

        private void wnd_Loaded(object sender, RoutedEventArgs e)
        {
            color();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);

            l = new Line();
            l.Y1 = 40;
            l.X1 = 270;
            l.Y2 = 290;
            l.X2 = 270;
            l.Stroke = rectrand;
            l.StrokeDashArray = new DoubleCollection() { 4, 10 };
            Cvs.Children.Add(l);

            strich1 = 10;
            strich = 4;

            ball = new Ball(ballfill, 270, 160, 200, 200, dlg.Radius);
            ball.Draw(Cvs);

            Timer = new Uhr(Cvs, uhrstroke, sekz, minz, stuz, uhrfill);

            player1 = new Paddel(100, 125, 15, 80, 10, paddle, rect);
            player2 = new Paddel(430, 125, 15, 80, 10, paddle, rect);

            player1.draw(Cvs);
            player2.draw(Cvs);

            l_Player_One_Name.Content = dlg.Player_one.Text;
            if (dlg.check_paddel.IsChecked.Value)
            {
                l_Player_Two_Name.Content = "Computer";
                autopaddel = true;
            }
            else
            {
                l_Player_Two_Name.Content = dlg.Player_two.Text;
                autopaddel = false;
            }
        }

        private void B_speed_Click(object sender, RoutedEventArgs e)
        {
            ball.changespeed(Slider_speed.Value);
            scaling = Slider_speed.Value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;

            ball.Move(ticks - ticks_old);

            checkscore();


            ball.Collision(rect);

            #region paddle
            ball.CollisionPaddle(player1.rect);
            ball.CollisionPaddle(player2.rect);

            if (Keyboard.IsKeyDown(Key.S) && player1.canmovedown)
            {
                player1.move(ticks - ticks_old, 15);
                box_s.Background = Brushes.Green;
                box_w.Background = background;
            }
            else if (Keyboard.IsKeyDown(Key.W) && player1.canmoveup)
            {
                player1.move(ticks - ticks_old, -15);
                box_s.Background = background;
                box_w.Background = Brushes.Green;
            }
            else
            {
                box_w.Background = background;
                box_s.Background = background;
            }
            if (!autopaddel) { 
                if (Keyboard.IsKeyDown(Key.Down) && player2.canmovedown)
                {
                    player2.move(ticks - ticks_old, 15);
                    box_up.Background = background;
                    box_down.Background = Brushes.Green;
                }
                else if (Keyboard.IsKeyDown(Key.Up) && player2.canmoveup)
                {
                    player2.move(ticks - ticks_old, -15);
                    box_up.Background = Brushes.Green;
                    box_down.Background = background;
                }
                else
                {
                    box_up.Background = background;
                    box_down.Background = background;
                }
            }else{
                if (ball.Vx > 0)
                {
                    double 
                }
            }
            #endregion

            if (score1 == dlg.maxscore)
            {
                timer.Stop();
                MessageBox.Show(l_Player_One_Name.Content + "hat gewonnen", "Win", MessageBoxButton.OK, MessageBoxImage.Information);
                neues_spiel();
            }
            else if (score2 == dlg.maxscore)
            {
                timer.Stop();
                MessageBox.Show(l_Player_Two_Name.Content + "hat gewonnen", "Win", MessageBoxButton.OK, MessageBoxImage.Information);
                neues_spiel();
            }

            double fps = (1 / (ticks - ticks_old) * 1000);
            fps = Math.Round(fps);
            l_fps.Content = fps + " FPS";

            ticks_old = ticks;
        }

        private void checkscore()
        {
            ball.checkscore(l_score_p1, l_score_p2, rect, score1, score2);
            if (score1 != Convert.ToInt32(l_score_p1.Content) || score2 != Convert.ToInt32(l_score_p2.Content))
            {
                score1 = Convert.ToInt32(l_score_p1.Content);
                score2 = Convert.ToInt32(l_score_p2.Content);
            }
        }

        

        private void start_Click(object sender, RoutedEventArgs e)
        {
            ticks_old = Environment.TickCount;
            Timer.timer.Start();
            timer.Start();
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void parameter_Click(object sender, RoutedEventArgs e)
        {
            neues_spiel();
        }

        private void neues_spiel()
        {
            Hide();
            timer.Stop();
            Timer.timer.Stop();
            MainWindow mw = new MainWindow();
            try
            {
                mw.Show();
            }
            catch { }
            Close();
        }


        private void Cvs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                ball.Resize(sx, sy);

                rect.Width *= sx;
                rect.Height *= sy;
                Canvas.SetLeft(rect, sx * Canvas.GetLeft(rect));
                Canvas.SetTop(rect, sy * Canvas.GetTop(rect));

                player1.resize(sx, sy, rect);
                player2.resize(sx, sy, rect);

                l.X1 *= sx;
                l.X2 *= sx;
                l.Y1 *= sy;
                l.Y2 *= sy;
                strich *= sy;
                strich1 *= sy;
                l.StrokeThickness *= sx;
                l.StrokeDashArray = new DoubleCollection() { strich, strich1 };

                box_resize(sx, sy, box_down);
                box_resize(sx, sy, box_left);
                box_resize(sx, sy, box_right);
                box_resize(sx, sy, box_s);
                box_resize(sx, sy, box_up);
                box_resize(sx, sy, box_w);
                box_resize(sx, sy, box_score_p1);
                box_resize(sx, sy, box_score_p2);
                box_resize(sx,sy,b_playertwo);
                box_resize(sx, sy, b_pplayerone);
                box_resize(sx, sy, b_vs);

                l_Player_One_Name.FontSize *= (sx + sy) / 2;
                l_Player_Two_Name.FontSize *= (sx + sy) / 2;
                l_vs.FontSize *= (sx + sy) / 2;
                l_score_p1.FontSize *= (sx + sy) / 2;
                l_score_p2.FontSize *= (sx + sy) / 2;
                l_down.FontSize *= (sx + sy) / 2;
                l_up.FontSize *= (sx + sy) / 2;
                l_s.FontSize *= (sx + sy) / 2;
                l_w.FontSize *= (sx + sy) / 2;
                L_speed_max.FontSize *= (sx + sy) / 2;
                l_speed_min.FontSize *= (sx + sy) / 2;

                L_speed_max.Width *= sx;
                L_speed_max.Height *= sy;
                Canvas.SetLeft(L_speed_max, sx * Canvas.GetLeft(L_speed_max));
                Canvas.SetTop(L_speed_max, sy * Canvas.GetTop(L_speed_max));

                l_speed_min.Width *= sx;
                l_speed_min.Height *= sy;
                Canvas.SetLeft(l_speed_min, sx * Canvas.GetLeft(l_speed_min));
                Canvas.SetTop(l_speed_min, sy * Canvas.GetTop(l_speed_min));

                l_fps.FontSize *= (sx + sy) / 2;
                l_fps.Width *= sx;
                l_fps.Height *= sy;
                Canvas.SetLeft(l_fps, sx * Canvas.GetLeft(l_fps));
                Canvas.SetTop(l_fps, sy * Canvas.GetTop(l_fps));


                Timer.resize(sx, sy);

                b_speed.Width *= sx;
                b_speed.Height *= sy;
                Canvas.SetLeft(b_speed, sx * Canvas.GetLeft(b_speed));
                Canvas.SetTop(b_speed, sy * Canvas.GetTop(b_speed));

                Slider_speed.Width *= sy;
                Canvas.SetLeft(Slider_speed, sx * Canvas.GetLeft(Slider_speed));
                Canvas.SetTop(Slider_speed, sy * Canvas.GetTop(Slider_speed));
            }
            catch { }
        }
        #region color
        private void color()
        {
            switch (dlg.SelectedColorShema)
            {
                case 0:
                    paddle = Brushes.Red;
                    rectrand = Brushes.DarkGray;
                    uhrfill = Brushes.White;
                    uhrstroke = Brushes.Black;
                    ballfill = Brushes.Blue;
                    hud = Brushes.Black;
                    sekz = Brushes.Black;
                    minz = Brushes.Black;
                    stuz = Brushes.Black;
                    background = Brushes.White;
                    break;
                case 1:
                    paddle = Brushes.Blue;
                    rectrand = Brushes.Blue;
                    uhrfill = Brushes.Black;
                    uhrstroke = Brushes.Blue;
                    ballfill = Brushes.Blue;
                    hud = Brushes.Blue;
                    sekz = Brushes.Blue;
                    minz = Brushes.Blue;
                    stuz = Brushes.Blue;
                    background = Brushes.Black;
                    break;
                case 2:
                    paddle = darkmode;
                    rectrand = darkmode;
                    uhrfill = (Brush)brush.ConvertFromString("#181818");
                    uhrstroke = darkmode;
                    ballfill = darkmode;
                    hud = darkmode;
                    sekz = darkmode;
                    minz = darkmode;
                    stuz = darkmode;
                    background = (Brush)brush.ConvertFromString("#181818");
                    break;
                case 3:
                    paddle = (Brush)brush.ConvertFromString(dlg.ClrPcker_Paddel.SelectedColorText);
                    rectrand = (Brush)brush.ConvertFromString(dlg.ClrPcker_Spielfeldrand.SelectedColorText);
                    uhrfill = (Brush)brush.ConvertFromString(dlg.ClrPcker_Uhrfill.SelectedColorText);
                    uhrstroke = (Brush)brush.ConvertFromString(dlg.ClrPcker_Uhrstroke.SelectedColorText);
                    ballfill = (Brush)brush.ConvertFromString(dlg.ClrPcker_Ball.SelectedColorText);
                    hud = (Brush)brush.ConvertFromString(dlg.ClrPcker_hud.SelectedColorText);
                    sekz = (Brush)brush.ConvertFromString(dlg.ClrPcker_Sekz.SelectedColorText);
                    minz = (Brush)brush.ConvertFromString(dlg.ClrPcker_Minz.SelectedColorText);
                    stuz = (Brush)brush.ConvertFromString(dlg.ClrPcker_Stuz.SelectedColorText);
                    background = (Brush)brush.ConvertFromString(dlg.ClrPcker_Background.SelectedColorText);
                    break;
            }
            boxclor(box_down, hud);
            boxclor(box_left, hud);
            boxclor(box_right, hud);
            boxclor(box_s, hud);
            boxclor(box_up, hud);
            boxclor(box_w, hud);
            boxclor(box_score_p1, hud);
            boxclor(box_score_p2, hud);
            boxclor(b_playertwo, hud);
            boxclor(b_pplayerone, hud);
            boxclor(b_vs, hud);

            l_Player_One_Name.Foreground = hud;
            l_Player_Two_Name.Foreground = hud;
            l_vs.Foreground = hud;
            l_score_p1.Foreground = hud;
            l_score_p2.Foreground = hud;
            l_down.Foreground = hud;
            l_s.Foreground = hud;
            l_up.Foreground = hud;
            l_w.Foreground = hud;
            L_speed_max.Foreground = hud;
            l_speed_min.Foreground = hud;
            l_fps.Foreground = hud;

            b_speed.BorderBrush = rectrand;
            b_speed.Background = background;
            b_speed.Foreground = hud;

            rect.Stroke = rectrand;
            rect.Fill = background;
            Cvs.Background = background;

            color_menu(Header);
            color_menu(start);
            color_menu(parameter);
            color_menu(ende);

            MainMenu.BorderBrush = hud;
            MainMenu.Background = hud;
        }

        private void box_resize(double sx, double sy, Border box)
        {
            box.Width *= sx;
            box.Height *= sy;

            Thickness thick = new Thickness();
            thick.Bottom = box.BorderThickness.Bottom * sy;
            thick.Top = box.BorderThickness.Top * sx;
            thick.Left = box.BorderThickness.Left * sx;
            thick.Right = box.BorderThickness.Right * sy;

            box.BorderThickness = thick;

            Canvas.SetLeft(box, sx * Canvas.GetLeft(box));
            Canvas.SetTop(box, sy * Canvas.GetTop(box));
        }
        private void boxclor(Border box, Brush color)
        {
            box.BorderBrush = color;
        }
        private void color_menu(MenuItem menu)
        {
            menu.Background = background;
            menu.Foreground = hud;
            menu.BorderBrush = hud;
        }
        #endregion
    }
}

