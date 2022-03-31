using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfZad4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point basePosition = new Point();
        Rectangle currentRectangle;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                basePosition = e.GetPosition(this);

                currentRectangle = new Rectangle();
                currentRectangle.Height = 1;
                currentRectangle.Width = 1; 

                // random rectangle color
                Random rnd = new Random();
                Color color = Color.FromRgb((byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255));
                currentRectangle.Stroke = new SolidColorBrush(color);
                currentRectangle.Fill = new SolidColorBrush(color); 
                currentRectangle.StrokeThickness = 1;

                Canvas.Children.Add(currentRectangle);
                Canvas.SetTop(currentRectangle, basePosition.Y);
                Canvas.SetLeft(currentRectangle, basePosition.X);
            }    
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Canvas.ReleaseMouseCapture();
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.CaptureMouse();
                var cursorPosition = e.MouseDevice.GetPosition(Canvas);   

                var currentX = Canvas.GetLeft(currentRectangle);
                var currentY = Canvas.GetTop(currentRectangle);

                if (cursorPosition.X - basePosition.X >= 0)
                {
                    currentRectangle.Width = cursorPosition.X - basePosition.X;
                }
                else
                {
                    Canvas.SetLeft(currentRectangle, cursorPosition.X);
                    currentRectangle.Width = basePosition.X - cursorPosition.X;
                }

                if (cursorPosition.Y - basePosition.Y >= 0)
                {
                    currentRectangle.Height = cursorPosition.Y - basePosition.Y;
                }
                else
                {
                    Canvas.SetTop(currentRectangle, cursorPosition.Y);
                    currentRectangle.Height = basePosition.Y - cursorPosition.Y; 
                }
            }
        }
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (currentRectangle == null)
                return;

            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                switch (e.Key)
                {
                    case Key.Up:
                        Canvas.SetTop(currentRectangle, Canvas.GetTop(currentRectangle) - 1);
                        currentRectangle.Height += 2;
                        break;

                    case Key.Down:
                        if (currentRectangle.Height - 2 > 5)
                        {
                            Canvas.SetTop(currentRectangle, Canvas.GetTop(currentRectangle) + 1);
                            currentRectangle.Height -= 2;
                        }
                        break;

                    case Key.Left:
                        if ( currentRectangle.Width - 2 > 5)
                        {
                            Canvas.SetLeft(currentRectangle, Canvas.GetLeft(currentRectangle) + 1);
                            currentRectangle.Width -= 2;
                        }
                        break;

                    case Key.Right:
                        Canvas.SetLeft(currentRectangle, Canvas.GetLeft(currentRectangle) - 1);
                        currentRectangle.Width += 2;
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        Canvas.SetTop(currentRectangle, Canvas.GetTop(currentRectangle) - 1);
                        break;

                    case Key.Down:
                        Canvas.SetTop(currentRectangle, Canvas.GetTop(currentRectangle) + 1);
                        break;

                    case Key.Left:
                        Canvas.SetLeft(currentRectangle, Canvas.GetLeft(currentRectangle) - 1);
                        break;

                    case Key.Right:
                        Canvas.SetLeft(currentRectangle, Canvas.GetLeft(currentRectangle) + 1);
                        break;
                }
            }
        }
    }
}
