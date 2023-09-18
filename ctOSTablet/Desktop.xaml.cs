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
            this.Opacity = 0;
            desktopBG.Visibility = Visibility.Visible;
            gMain.Children.Add(new ctOSWindow());
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
