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
    /// Security.xaml 的交互逻辑
    /// </summary>
    public partial class Security : Page
    {
        public Security()
        {
            InitializeComponent();
            (Application.Current as App).sysEvent.LoginSuccessful += SysEvent_LoginSuccessful;
            
        }

        private void SysEvent_LoginSuccessful()
        {
            DoubleAnimation fadeOut = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new PowerEase() { Power = 2, EasingMode = EasingMode.EaseIn }
            };

            this.BeginAnimation(OpacityProperty, fadeOut);
        }

        private async void login_Loaded(object sender, RoutedEventArgs e)
        {
            login.Visibility = Visibility.Hidden;
            login.SetWndContent(new LoginContent());
            await Task.Delay(500);
            login.Visibility = Visibility.Visible;
        }
    }
}
