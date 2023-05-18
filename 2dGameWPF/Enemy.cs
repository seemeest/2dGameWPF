using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2dGameWPF
{
    public class Enemy
    {
        private Rectangle rectangle;
        private double moveSpeed = 3;
        public double Speed;

        public Enemy(double top, double left)
        {
            rectangle = new Rectangle();
            rectangle.Width = 30;
            rectangle.Height = 30;

            ImageBrush imageBrush = new ImageBrush();
            string imagePath = "pack://application:,,,/image/Enemi.png";
            imageBrush.ImageSource = new ImageSourceConverter().ConvertFromString(imagePath) as ImageSource;


            rectangle.Fill = imageBrush;

            Canvas.SetTop(rectangle, top);
            Canvas.SetLeft(rectangle, left);
            Speed = 1;
        }

        public Rectangle Get()
        {
            return rectangle;
        }

        public void Move()
        {
            double currentTop = Canvas.GetTop(rectangle);
            Canvas.SetTop(rectangle, currentTop + moveSpeed);
        }
    }

}
