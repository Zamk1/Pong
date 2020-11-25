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

namespace DlgMenuDemo
{
    class Uhr
    {
        Zeiger Sek, Min, Stu;
        public DispatcherTimer timer;
        double msek;
        int min, stu;
        Canvas c;
        Ellipse k;
        double ticks_old = 0;

        public Uhr(Canvas Cvs, Brush color, Brush zsek, Brush zmin, Brush zstu , Brush fill)
        {
            c = Cvs;

            msek = 0;
            min = 0;
            stu = 0;
            k = new Ellipse();
            k.StrokeThickness = 3;
            k.Stroke = color;
            k.Width = 2 * 100;
            k.Height = 2 * 100;
            k.Fill = fill;
            Canvas.SetLeft(k, 600 - 100);
            Canvas.SetTop(k, 150 - 100);
            Cvs.Children.Add(k);

            Sek = new Zeiger(90, 0, 600, 150, zsek);
            Sek.draw(Cvs);

            Min = new Zeiger(70, 0, 600, 150, zmin);
            Min.draw(Cvs);

            Stu = new Zeiger(50, 0, 600, 150, zstu);
            Stu.draw(Cvs);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            double ticks = Environment.TickCount;

            msek = msek + (ticks-ticks_old);
            if (msek >= 60000 && ticks_old != 0)
            {
                min++;
                msek = 0;
            }
            if (min == 60)
            {
                stu++;
                min = 0;
            }
            Sek.angle = Angle(1);
            Min.angle = Angle(2);
            Stu.angle = Angle(3);
            Sek.draw(c);
            Min.draw(c);
            Stu.draw(c);

            ticks_old = ticks;
        }

        public void undraw(Canvas c)
        {
            if (c.Children.Contains(k))
            {
                c.Children.Remove(k);
            }
            Min.undraw(c);
            Sek.undraw(c);
            Stu.undraw(c);
        }

        private double Angle(int z)
        {
            if (z == 1)
            {
                return (2 * Math.PI / 60000) * msek;
            }
            else if (z == 2)
            {
                return (2 * Math.PI / 60) * (min - 1);
            }
            else
            {
                return (2 * Math.PI / 12) * stu;
            }
        }

        public void resize(double sx, double sy)
        {
            k.Width *= (sx + sy) / 2;
            k.Height *= (sx + sy) / 2;
            k.StrokeThickness *= (sx + sy) / 2;

            Canvas.SetLeft(k, sx * Canvas.GetLeft(k));
            Canvas.SetTop(k, sy * Canvas.GetTop(k));

            Sek.resize(Canvas.GetLeft(k) + (k.Width / 2), Canvas.GetTop(k) + (k.Width / 2), k.Width / 2, 10, sx, sy);
            Min.resize(Canvas.GetLeft(k) + (k.Width / 2), Canvas.GetTop(k) + (k.Width / 2), k.Width / 2, 30, sx, sy);
            Stu.resize(Canvas.GetLeft(k) + (k.Width / 2), Canvas.GetTop(k) + (k.Width / 2), k.Width / 2, 50, sx, sy);
        }
    }
}
