using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    class Ball
    {
        public Ellipse Elli { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }
        public Double Radius { get; set; }

        private double scalingold = 1;

        public Ball(Brush color , Double X = 100, Double Y = 100, Double Vx = 20, Double Vy = 20, Double Radius = 10)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.Radius = Radius;

            Elli = new Ellipse();
            Elli.Width = 2 * Radius;
            Elli.Height = 2 * Radius;
            Elli.Fill = color;

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Elli))
            {
                c.Children.Add(Elli);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Elli))
            {
                c.Children.Remove(Elli);
            }
        }

        public void Resize(double sx, double sy)
        {
            X *= sx;
            Y *= sy;

            Vx *= sx;
            Vy *= sy;

            Radius *= (sx + sy) / 2;

            Elli.Width *= (sx + sy) / 2;
            Elli.Height *= (sx + sy) / 2;

            Canvas.SetLeft(Elli, sx * Canvas.GetLeft(Elli));
            Canvas.SetTop(Elli, sy * Canvas.GetTop(Elli));
        }

        public void Move(Double dt)
        {
            X = X + Vx * dt / 1000 * scalingold;
            Y = Y + Vy * dt / 1000 * scalingold;
        }

        public void Collision(Rectangle r)
        {
            // Obere oder untere Bande
            if (Y - Radius <= Canvas.GetTop(r))
            {
                Vy = -Vy;                                       // Reflexion and der Bande
                Y = Y + 2 * (Canvas.GetTop(r) - (Y - Radius));  // Korrektur des Detektionsfehlers
            }
            else if (Y + Radius >= Canvas.GetTop(r) + r.Height)
            {
                Vy = -Vy;
                Y = Y - 2 * (Y + Radius - Canvas.GetTop(r) - r.Height);
            }

            // Linke oder rechte Bande
            if (X - Radius <= Canvas.GetLeft(r))
            {
                Vx = -Vx;
                X = X + 2 * (Canvas.GetLeft(r) - (X - Radius));
            }
            else if (X + Radius >= Canvas.GetLeft(r) + r.Width)
            {
                Vx = -Vx;
                X = X - 2 * (X + Radius - Canvas.GetLeft(r) - r.Width);
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }

        public void checkscore(Label lscore, Label rscore, Rectangle rand,int l_score,int r_score)
        {
            if (X - Radius <= Canvas.GetLeft(rand))
            {
                l_score++;
                lscore.Content = Convert.ToString(l_score);
            }else if(X + Radius >= Canvas.GetLeft(rand) + rand.Width)
            {
                r_score++;
                rscore.Content = Convert.ToString(r_score);
            }
        }

        public void CollisionPaddle(Rectangle paddle)
        {
            if (X - Radius <= Canvas.GetLeft(paddle) + paddle.Width && X - Radius > Canvas.GetLeft(paddle) && Y + Radius > Canvas.GetTop(paddle) && Y - Radius < Canvas.GetTop(paddle) + paddle.Height && Vx < 0)
            {
                Vx = -Vx;
                X = X + Radius;
                //X = X + 2 * (Canvas.GetLeft(paddle) - (X - Radius));
            }
            if (X + Radius >= Canvas.GetLeft(paddle) && X + Radius < Canvas.GetLeft(paddle) + paddle.Width && Y + Radius > Canvas.GetTop(paddle) && Y - Radius < Canvas.GetTop(paddle) + paddle.Height && Vx > 0)
            {
                Vx = -Vx;
                X = X - Radius;
                //X = X - 2 * (X + Radius - Canvas.GetLeft(paddle) - paddle.Width);
            }
            if (Y + Radius >= Canvas.GetTop(paddle) && Y + Radius < Canvas.GetTop(paddle) + paddle.Height && X + Radius > Canvas.GetLeft(paddle) && X - Radius < Canvas.GetLeft(paddle) + paddle.Width )
            {
                Vy = -Vy;
                Y = Y - Radius;
            }
            if (Y - Radius <= Canvas.GetTop(paddle) + paddle.Height && Y - Radius > Canvas.GetTop(paddle) && X + Radius > Canvas.GetLeft(paddle) && X - Radius < Canvas.GetLeft(paddle) + paddle.Width)
            {
                Vy = -Vy;
                Y = Y + Radius;
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }

        public void changespeed(double scaling)
        {
            scalingold = scaling;
        }
    }
}

