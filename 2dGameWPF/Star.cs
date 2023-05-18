using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2dGameWPF
{
    public class Star
    {
        Rectangle rectangle;
        Timer timer;
        Canvas canvas;

        public Star(Canvas canvas)
        {
            this.canvas = canvas;
            rectangle = new Rectangle();
            Random random = new Random();

            var val = 2;
            rectangle.Width = val;
            rectangle.Height = val;
            Brush brush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            rectangle.Fill = brush;



            timer = new Timer(StartFallingAnimation, null, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100));
        }



        private void StartFallingAnimation(object state)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                // Получение текущих координат элемента на Canvas
                //double currentX = Canvas.GetLeft(rectangle);
                double currentY = Canvas.GetTop(rectangle);

            // Вычисление новых координат элемента для перемещения вниз
            double newY = currentY + 4;

                // Установка новых координат элемента на Canvas
                Random random = new Random();
                double opacity = random.NextDouble() * (1 - 0.4) + 0.4;
                rectangle.Opacity = opacity;
                if (newY > 450) { Canvas.SetTop(rectangle, 0); return; }
                Canvas.SetTop(rectangle, newY);
                
            });
        }


        private void AnimationCompleted(object sender, EventArgs e)
        {
            // Удалить прямоугольник из Canvas или выполнить другие действия по окончании анимации
            canvas.Children.Remove(rectangle);
        }

        public Rectangle GetStar()
        {
            return rectangle;
        }
    }

}
