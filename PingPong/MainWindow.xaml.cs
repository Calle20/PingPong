using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PingPong
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer animationsTimer = new DispatcherTimer();
        private bool Gehenachrechts = true;
        private bool Gehenachunten = true;
        private int Zähler = 0;
        private int Zähler2 = 0;
        private Key Player1Up { get; set; }
        private Key Player1Down { get; set; }
        private Key Player2Up { get; set; }
        private Key Player2Down { get; set; }
        private bool VsBot { get; set; }
        private bool StartBot{ get; set; }
        private bool Lose { get; set; }
        private bool Player1goup { get; set; }
        private bool Player2goup { get; set; }
        private bool Player1godown { get; set; }
        private bool Player2godown { get; set; }
        private static bool Colission { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            SliderSpeed.Value = Properties.Settings.Default.Speed;
            animationsTimer.Interval = TimeSpan.FromMilliseconds(Properties.Settings.Default.Speed);
            VsBot = false;
            animationsTimer.Tick += PositioniereBall;
        }
        public static bool CheckCollisionLeft(Rectangle e1, Rectangle e2)
        {
            double x1 = Canvas.GetLeft(e1);
            double y1 = Canvas.GetTop(e1);
            Rect r1 = new Rect(x1, y1, e1.ActualWidth, e1.ActualHeight);

            double x2 = Canvas.GetLeft(e2);
            double y2 = Canvas.GetTop(e2);
            Rect r2 = new Rect(x2, y2, e2.ActualWidth, e2.ActualHeight);
            return r1.IntersectsWith(r2);
        }
        public static bool CheckCollisionRight(Rectangle e1, Rectangle e2)
        {
            double x1 = Canvas.GetLeft(e1) - (e1.ActualWidth / 2);
            double y1 = Canvas.GetTop(e1);
            Rect r1 = new Rect(x1, y1, e1.ActualWidth, e1.ActualHeight);

            double x2 = Canvas.GetLeft(e2);
            double y2 = Canvas.GetTop(e2);
            Rect r2 = new Rect(x2, y2, e2.ActualWidth, e2.ActualHeight);
            return r1.IntersectsWith(r2);
        }
        private void PositioniereBall(object sender, EventArgs e)
        {
            if (Canvas.GetLeft(Mittellinie) >= Canvas.GetLeft(Ball))
            {
                if (StartBot)
                {
                    StartBot = false;
                    Colission = false;
                }
                if (CheckCollisionLeft(PlayerLeft, Ball))
                {
                    Gehenachrechts = true;
                }
            }
            else
            {
                if (!StartBot)
                {
                    StartBot = true;
                    Start_Bot();
                }
                if (VsBot)
                {
                    Position_Bot();
                }
                if (CheckCollisionRight(PlayerRight, Ball))
                {
                    Gehenachrechts = false;
                    Colission = true;
                }
            }
            double x = Canvas.GetLeft(Ball);
            if (x >= Spielfeld.ActualWidth - Ball.ActualWidth)
            {
                Zähler++;
                btnStartStop_Click(btnStartStop, new RoutedEventArgs());
            }
            else if (x <= 0)
            {
                Zähler++;
                btnStartStop_Click(btnStartStop, new RoutedEventArgs());
            }
            if (Gehenachrechts)
            {
                Canvas.SetLeft(Ball, x + 6);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 6);
            }
            double y = Canvas.GetTop(Ball);
            if (y >= Spielfeld.ActualHeight - Ball.ActualHeight)
            {
                Gehenachunten = false;
            }
            else if (y <= 0)
            {
                Gehenachunten = true;
            }
            if (Gehenachunten)
            {
                Canvas.SetTop(Ball, y + 6);
            }
            else
            {
                Canvas.SetTop(Ball, y - 6);
            }
            double x2 = Canvas.GetTop(PlayerLeft);
            if (Player1godown && x2 <= Spielfeld.ActualHeight - PlayerLeft.ActualHeight - 10) 
            {
                Canvas.SetTop(PlayerLeft, x2 + 12);
            }
            if (Player1goup && x2 >= 5)
            {
                Canvas.SetTop(PlayerLeft, x2 - 12);
            }
            double y2 = Canvas.GetTop(PlayerRight);
            if (Player2godown && !VsBot && y2 <= Spielfeld.ActualHeight - PlayerRight.ActualHeight - 10)
            {
                Canvas.SetTop(PlayerRight, y2 + 12);
            }
            if (Player2goup && !VsBot && y2 >= 5)
            {
                Canvas.SetTop(PlayerRight, y2 - 12);
            }
        }
        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (animationsTimer.IsEnabled)
            {
                animationsTimer.Stop();
                Ball.Visibility = Visibility.Hidden;
                lblLose.Content = Zähler;
                if (VsBot)
                {
                    lblLose1.Content = "Spieler: " + Zähler;
                    lblLose2.Content = "Computer: " + Zähler2;
                }
                else
                {
                    lblLose1.Content = "Spieler1: " + Zähler;
                    lblLose2.Content = "Spieler2: " + Zähler2;
                }
            }
            else if (!animationsTimer.IsEnabled)
            {
                Random r = new Random();
                int PosTop = r.Next(5, Convert.ToInt32(Spielfeld.ActualHeight));
                int Way = r.Next(0, 2);
                double PosLeft = Spielfeld.ActualWidth / 2;
                Canvas.SetTop(Ball, PosTop);
                Canvas.SetLeft(Ball, PosLeft);
                if (Way == 0)
                {
                    Gehenachrechts = true;
                }
                if (Way == 1)
                {
                    Gehenachrechts = false;
                }
                Ball.Visibility = Visibility.Visible;
                animationsTimer.Start();
                Thread.Sleep(500);
            }
        }
        private void Start_Bot()
        {
            Random r = new Random();
            int b = r.Next(0, 11);
            if (b <= 7)
            {
                Lose = false;
            }
            else
            {
                Lose = true;
            }
        }
        private void Position_Bot()
        {
            double x = Canvas.GetTop(Ball);
            double top = Canvas.GetTop(PlayerRight);
            double down = top + PlayerRight.ActualHeight;
            if (!Colission)
            {
                if (Lose)
                {
                    if (top >= 10)
                    {
                        Canvas.SetTop(PlayerRight, Spielfeld.ActualHeight - x + PlayerRight.ActualHeight);
                    }
                    else
                    {
                        Canvas.SetTop(PlayerRight, Spielfeld.ActualHeight - x + PlayerRight.ActualHeight);
                    }
                }
                else
                {
                    Canvas.SetTop(PlayerRight, x - (PlayerRight.ActualHeight / 2));
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Player1Down)
            {
                Player1godown = true;
            }
            if (e.Key == Player1Up)
            {
                Player1goup = true;
            }
            if (e.Key == Player2Down && !VsBot)
            {
                Player2godown = true;
                //double x = Canvas.GetTop(PlayerRight);
                //if (x <= Spielfeld.ActualHeight - PlayerRight.ActualHeight - 10)
                //{
                //    Canvas.SetTop(PlayerRight, x + 12);
                //}
            }
            if (e.Key == Player2Up && !VsBot)
            {
                Player2goup = true;
                //double x = Canvas.GetTop(PlayerRight);
                //if (x >= 5)
                //{
                //    Canvas.SetTop(PlayerRight, x - 12);
                //}
            }
        }
        private void SinglePlayer(object sender, RoutedEventArgs e)
        {
            lblLose.Content = 0;
            Zähler = 0;
            Zähler2 = 0;
            gbLose1vs1.Visibility = Visibility.Hidden;
            gbLoseSingleplayer.Visibility = Visibility.Visible;
        }
        private void _1VS1(object sender, RoutedEventArgs e)
        {
            Zähler = 0;
            Zähler2 = 0;
            lblLose1.Content = "Spieler 1: ";
            lblLose2.Content = "Spieler 2: ";
            gbLose1vs1.Visibility = Visibility.Visible;
            gbLoseSingleplayer.Visibility = Visibility.Hidden;
        }
        private void VSBot(object sender, RoutedEventArgs e)
        {
            Zähler = 0;
            Zähler2 = 0;
            VsBot = true;
            lblLose1.Content = "Spieler: ";
            lblLose2.Content = "Computer: ";
            gbLose1vs1.Visibility = Visibility.Visible;
            gbLoseSingleplayer.Visibility = Visibility.Hidden;
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            Zähler = 0;
            lblLose.Content = 0;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rbSinglePlayer.IsChecked = true;
            SetKeys();
        }
        private void Key_Change(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.ShowDialog();
            SetKeys();
        }
        private void Reset1vs1(object sender, RoutedEventArgs e)
        {
            Zähler = 0;
            Zähler2 = 0;
            if (VsBot)
            {
                lblLose1.Content = "Spieler: " + Zähler;
                lblLose2.Content = "Computer: " + Zähler2;
            }
            else
            {
                lblLose1.Content = "Spieler1: " + Zähler;
                lblLose2.Content = "Spieler2: " + Zähler2;
            }
        }
        private void SetKeys()
        {
            Player1Up = (Key)Enum.ToObject(typeof(Key), Properties.Settings.Default.Player1Up);
            Player1Down = (Key)Enum.ToObject(typeof(Key), Properties.Settings.Default.Player1Down);
            Player2Up = (Key)Enum.ToObject(typeof(Key), Properties.Settings.Default.Player2Up);
            Player2Down = (Key)Enum.ToObject(typeof(Key), Properties.Settings.Default.Player2Down);
        }
        private void SliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Properties.Settings.Default.Speed = SliderSpeed.Value;
            Properties.Settings.Default.Save();
            animationsTimer.Interval = TimeSpan.FromMilliseconds(Properties.Settings.Default.Speed);
        }
        private void rbvsbot_Unchecked(object sender, RoutedEventArgs e)
        {
            VsBot = false;
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Player1Down)
            {
                Player1godown = false;
            }
            if (e.Key == Player1Up)
            {
                Player1goup = false;
            }
            if (e.Key == Player2Down && !VsBot)
            {
                Player2godown = false;
            }
            if (e.Key == Player2Up && !VsBot)
            {
                Player2goup = false;
            }
        }
    }
}