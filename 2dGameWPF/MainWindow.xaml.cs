using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;

namespace _2dGameWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Star> stars = new List<Star>();
        private DispatcherTimer timer;
        private List<string> imagePaths;
        private int currentIndex;
        private string currentImagePath;
        private List<Shoot> shoots = new List<Shoot>();
        private DispatcherTimer timerShoot;
        public MainWindow()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            InitializeComponent();
            InitializeStars();
            InitializeTimer();
            StartShootsAnimation();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения загрузки: {elapsed.TotalMilliseconds} мс");

        }
       

      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            CreateEnemies(); // Добавляем врагов
            StartEnemiesAnimation(); // Запускаем анимацию врагов
        }

        private List<Enemy> enemies = new List<Enemy>();
        private DispatcherTimer timerEnemies;

        private void CreateEnemies()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double canvasWidth = _Canvas.ActualWidth;
            double enemySpacing = 50; // Расстояние между врагами
            int numberOfEnemies = 5; // Начальное количество врагов
            double enemySpeed = 1; // Начальная скорость врагов

            // Увеличение сложности в зависимости от количества врагов
            if (enemies.Count >= 10)
            {
                numberOfEnemies = 10; // Увеличиваем количество врагов
                enemySpeed = 2; // Увеличиваем скорость врагов
            }
            else if (enemies.Count >= 5)
            {
                numberOfEnemies = 7; // Увеличиваем количество врагов
                enemySpeed = 1.5; // Увеличиваем скорость врагов
            }

            for (int i = 0; i < numberOfEnemies; i++)
            {
                Enemy enemy = new Enemy(0, i * enemySpacing);
                enemy.Speed = enemySpeed; // Назначаем скорость врага
                enemies.Add(enemy);
                _Canvas.Children.Add(enemy.Get());
            }
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения CreateEnemies : {elapsed.TotalMilliseconds} мс");
        }


        private void StartEnemiesAnimation()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            timerEnemies = new DispatcherTimer();
            timerEnemies.Interval = TimeSpan.FromMilliseconds(20);
            timerEnemies.Tick += Timer_TickEnemies;
            timerEnemies.Start();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения StartEnemiesAnimation : {elapsed.TotalMilliseconds} мс");
        }

        private void Timer_TickEnemies(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (Enemy enemy in enemies.ToList())
            {
                enemy.Move();

                // Проверка столкновения врага с игроком
                if (IsCollision(enemy.Get(), player))
                {
                    // Обработка столкновения (например, конец игры)
                    MessageBox.Show("Game Over");
                }

                // Проверка позиции врага
                if (Canvas.GetTop(enemy.Get()) >= _Canvas.ActualHeight)
                {
                    // Удаляем врага и создаем новую волну
                    _Canvas.Children.Remove(enemy.Get());
                    enemies.Remove(enemy);

                    // Проверяем, остались ли еще враги
                    if (enemies.Count == 0)
                    {
                        // Создаем новую волну врагов
                        CreateEnemies();
                    }
                }
            }
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения Timer_TickEnemies : {elapsed.TotalMilliseconds} мс");
        }


        private bool IsCollision(FrameworkElement element1, FrameworkElement element2)
        {
            Rect rect1 = new Rect(Canvas.GetLeft(element1), Canvas.GetTop(element1), element1.ActualWidth, element1.ActualHeight);
            Rect rect2 = new Rect(Canvas.GetLeft(element2), Canvas.GetTop(element2), element2.ActualWidth, element2.ActualHeight);
            return rect1.IntersectsWith(rect2);
        }

        private void InitializeStars()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Random random = new Random();

            for (int i = 0; i < 50; i++)
            {
                Star star = new Star(_Canvas);
                stars.Add(star);

                _Canvas.Children.Add(star.GetStar());
                Canvas.SetZIndex(star.GetStar(), 0);
                Canvas.SetTop(star.GetStar(), random.NextDouble() * 450);
                Canvas.SetLeft(star.GetStar(), random.NextDouble() * 400);
            }
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения InitializeStars : {elapsed.TotalMilliseconds} мс");
        }

        private void InitializeTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            imagePaths = new List<string>
        {
            "/image/spaceship1.png",
            "/image/spaceship2.png",
            "/image/spaceship1.png",
            "/image/spaceship2.png",
            // Добавьте другие пути к изображениям, если требуется
        };

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(600);
            timer.Tick += Timer_Tick;
            timer.Start();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения InitializeTimer : {elapsed.TotalMilliseconds} мс");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            currentIndex = (currentIndex + 1) % imagePaths.Count;
            string nextImagePath = imagePaths[currentIndex];

            if (currentImagePath != nextImagePath)
            {
                player.Source = new BitmapImage(new Uri(nextImagePath, UriKind.Relative));
                currentImagePath = nextImagePath;
            }
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения Timer_Tick : {elapsed.TotalMilliseconds} мс");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            double moveDistance = 10;

            if (e.Key == Key.A)
                MoveImage(-moveDistance, 0);
            else if (e.Key == Key.D)
                MoveImage(moveDistance, 0);
            else if (e.Key == Key.Space)
                Thread.Sleep(99999999);
        }

        private void MoveImage(double offsetX, double offsetY)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double currentX = Canvas.GetLeft(player);
            double currentY = Canvas.GetTop(player);
            double newX = currentX + offsetX;
            double newY = currentY + offsetY;

            double canvasWidth = _Canvas.ActualWidth;
            double canvasHeight = _Canvas.ActualHeight;
            double playerWidth = player.ActualWidth;
            double playerHeight = player.ActualHeight;

            newX = Math.Max(0, Math.Min(newX, canvasWidth - playerWidth));
            newY = Math.Max(0, Math.Min(newY, canvasHeight - playerHeight));

            Canvas.SetLeft(player, newX);
            Canvas.SetTop(player, newY);
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения MoveImage : {elapsed.TotalMilliseconds} мс");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double playerTop = Canvas.GetTop(player);
            double playerLeft = Canvas.GetLeft(player) + player.Width / 2;

            Shoot shoot = new Shoot(playerTop, playerLeft);
            shoots.Add(shoot);

            _Canvas.Children.Add(shoot.Get());
        }

        private void StartShootsAnimation()
        {
            timerShoot = new DispatcherTimer();
            timerShoot.Interval = TimeSpan.FromMilliseconds(10);
            timerShoot.Tick += Timer_TickShoot;
            timerShoot.Start();
        }

        private void Timer_TickShoot(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Shoot> shootsToRemove = new List<Shoot>();

            // Перемещение пуль и проверка столкновений
            foreach (Shoot shoot in shoots)
            {
                Rectangle shootRectangle = shoot.Get();
                double currentTop = Canvas.GetTop(shootRectangle);
                double newTop = currentTop - 5;
                Canvas.SetTop(shootRectangle, newTop);

                if (newTop <= 0)
                {
                    // Пуля достигла верхней границы
                    shootsToRemove.Add(shoot);
                    _Canvas.Children.Remove(shootRectangle);
                }
                else
                {
                    // Проверка столкновения пули с врагами
                    foreach (Enemy enemy in enemies)
                    {
                        if (IsCollision(shootRectangle, enemy.Get()))
                        {
                            // Пуля попала во врага
                            _Canvas.Children.Remove(shootRectangle);
                            shootsToRemove.Add(shoot);
                            _Canvas.Children.Remove(enemy.Get());
                            enemies.Remove(enemy);
                            break; // Прерываем цикл, чтобы избежать ошибки после удаления элементов из коллекции
                        }
                    }
                }
            }

            // Удаление пуль, попавших во врагов или достигших верхней границы
            foreach (Shoot shootToRemove in shootsToRemove)
            {
                shoots.Remove(shootToRemove);
            }

            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения Timer_TickShoot: {elapsed.TotalMilliseconds} мс");
        }



        public class Shoot
        {
            private Rectangle rectangle;

            public Shoot(double top, double left)
            {
                rectangle = new Rectangle();
                rectangle.Width = 3;
                rectangle.Height = 20;
                Brush brush = new SolidColorBrush(Color.FromRgb(244, 165, 61));
                rectangle.Fill = brush;

                Canvas.SetTop(rectangle, top);
                Canvas.SetLeft(rectangle, left);
            }

            public Rectangle Get()
            {
                return rectangle;
            }
        }

      
    }

}

