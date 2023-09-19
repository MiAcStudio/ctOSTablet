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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ctOSTablet
{
    /// <summary>
    /// Desktop.xaml 的交互逻辑
    /// </summary>
    public partial class Desktop : Page
    {
        public Desktop()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (object s, EventArgs e) => {
                lblTime.Content = DateTime.Now.ToString("HH:mm");
            };
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();

            this.Opacity = 0;
            desktopBG.Visibility = Visibility.Visible;
            gMain.Children.Add(new ctOSWindow());
            gMain.Children.Add(new ctOSWindow(new Terminal()));
            DoubleAnimation fadeIn = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new PowerEase() { Power = 2, EasingMode = EasingMode.EaseOut }
            };

            this.BeginAnimation(OpacityProperty, fadeIn);
        }

        private void btnStart_MouseMove(object sender, MouseEventArgs e)
        {
            bgStart.Fill = Brushes.DimGray;
        }

        private void btnStart_MouseLeave(object sender, MouseEventArgs e)
        {
            bgStart.Fill = Brushes.Black;
        }
    }
}
