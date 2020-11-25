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
    class Paddel
    {
        public Rectangle rect = new Rectangle();
        double maxtop, mintop, speed;
        public bool canmovedown, canmoveup;


        public Paddel(double x, double y, double width, double heigth, double speeed, Brush color, Rectangle rand)
        {
            rect.Height = heigth;
            rect.Width = width;

            canmovedown = true;
            canmoveup = true;

            speed = speeed;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            mintop = Canvas.GetTop(rand);
            maxtop = Canvas.GetTop(rand) + rand.Height - rect.Height;

            rect.Fill = color;
        }



        public void move(double dir, double delta_t)
        {
            Canvas.SetTop(rect, Canvas.GetTop(rect) + dir * speed * delta_t / 1000);
            if (Canvas.GetTop(rect) + dir * speed * delta_t / 1000 <= mintop)
            {
                canmoveup = false;
                canmovedown = true;
            }
            else if (Canvas.GetTop(rect) + dir * speed * delta_t / 1000 > maxtop)
            {
                canmovedown = false;
                canmoveup = true;
            }
            else
            {
                canmovedown = true;
                canmoveup = true;
            }
        }

        public void draw(Canvas c)
        {
            if (!c.Children.Contains(rect))
            {
                c.Children.Add(rect);
            }
        }

        public void undraw(Canvas c)
        {
            if (c.Children.Contains(rect))
            {
                c.Children.Remove(rect);
            }
        }

        public void resize(double sx, double sy, Rectangle Rand)
        {
            rect.Width *= sx;
            rect.Height *= sy;
            speed *= sy;
            mintop = Canvas.GetTop(Rand);
            maxtop = Canvas.GetTop(Rand) + Rand.Height - rect.Height;
            Canvas.SetLeft(rect, sx * Canvas.GetLeft(rect));
            Canvas.SetTop(rect, sy * Canvas.GetTop(rect));
        }
    }
}

