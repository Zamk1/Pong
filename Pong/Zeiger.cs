using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DlgMenuDemo
{
    class Zeiger
    {
        Line z = new Line();
        double length;
        public double angle;

        public Zeiger(double l, double a, double xx, double yy, Brush color)
        {
            z.X1 = xx;
            z.Y1 = yy;
            length = l;
            angle = a;
            z.Stroke = color;
            z.StrokeThickness = 2.5;
        }

        public void draw(Canvas c)
        {
            undraw(c);
            double gk, ak;
            gk = length * Math.Sin(angle);
            ak = length * Math.Cos(angle);
            z.X2 = z.X1 + gk;
            z.Y2 = z.Y1 - ak;

            if (!c.Children.Contains(z))
            {
                c.Children.Add(z);
            }
        }

        public void undraw(Canvas c)
        {
            if (c.Children.Contains(z))
            {
                c.Children.Remove(z);
            }
        }

        public void resize(double sx, double sx2, double laenge, double verschiebung, double scale_x, double scale_y)
        {
            length = laenge - verschiebung;
            z.X1 = sx;
            z.Y1 = sx2;
            double gk, ak;
            gk = length * Math.Sin(angle);
            ak = length * Math.Cos(angle);
            z.X2 = z.X1 + gk;
            z.Y2 = z.Y1 - ak;
            z.StrokeThickness *= (scale_x + scale_y) / 2;
        }
    }
}