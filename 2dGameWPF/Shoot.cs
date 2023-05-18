//using System;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using System.Windows;
//using System.Collections.Generic;
//using System.Windows.Threading;
//using System.Windows.Media.Imaging;
//using System.Dynamic;
//using System.Reflection;
//using System.Windows.Media.Animation;

//namespace _2dGameWPF
//{
//    public class Shoot
//    {
//        Rectangle rectangle { get; }

//        DispatcherTimer timer;
//        DispatcherTimer timerMove;
//        private Canvas canvas;

//        List<SolidColorBrush> colors;

       

//        private double initialTop; 
//        public Shoot(Canvas canvas) { 

//            this.canvas = canvas;
//            rectangle = new Rectangle();
//            rectangle.Width = 3 ;
//            rectangle.Height = 20;
//            Brush brush = new SolidColorBrush(Color.FromRgb(244, 165, 61));
//            rectangle.Fill = brush;

//            colors = new List<SolidColorBrush> {
//               new SolidColorBrush(Color.FromRgb(244, 165, 61)),
//               new SolidColorBrush(Color.FromRgb(246,177,72)),
//               new SolidColorBrush(Color.FromRgb(210,103,42)),
//               new SolidColorBrush(Color.FromRgb(218,144,55)),
//               new SolidColorBrush(Color.FromRgb(214,129,50))

//            };

//            rectangle.Tag = "Bullet";

//            timer = new DispatcherTimer();
//            timer.Interval = TimeSpan.FromMilliseconds(600); // Интервал времени между сменой изображений
//            timer.Tick += Timer_Tick;
//            timer.Start();
//            initialTop = Canvas.GetTop(rectangle);
            
//            timerMove= new DispatcherTimer();
//            timerMove.Tick += MoveShoot;
//            timerMove.Interval =TimeSpan.FromMilliseconds(10);
//            timerMove.Start();

//        }

//        private void MoveShoot(object sender, EventArgs e)
//        {
//            // Изменение координаты Top пули (движение вверх)
//            double currentTop = Canvas.GetTop(rectangle);
//            Canvas.SetTop(rectangle, currentTop - 5);

//            // Проверка, достигла ли пуля верхней границы Canvas
//            if (currentTop <= 0)
//            {
//                // Пуля достигла верхней границы, остановка таймера и удаление пули из Canvas
//                timer.Stop();
//                canvas.Children.Remove(rectangle);
//            }
//        }

//        private int colorIndex = 0; // Индекс текущего цвета

//        private void Timer_Tick(object sender, EventArgs e)
//        {
//            // Получение следующего цвета из списка
//            SolidColorBrush nextColor = colors[colorIndex % colors.Count];

//            // Создание анимации изменения цвета
//            ColorAnimation colorAnimation = new ColorAnimation();
//            colorAnimation.To = nextColor.Color;
//            colorAnimation.Duration = TimeSpan.FromMilliseconds(200);

//            // Применение анимации к свойству Fill у rectangle
//            rectangle.Fill.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

//            // Увеличение индекса для получения следующего цвета
//            colorIndex++;



//        }

//        public Rectangle Get()
//        {

//            return rectangle;
//        }
     
        
//    }
//}
