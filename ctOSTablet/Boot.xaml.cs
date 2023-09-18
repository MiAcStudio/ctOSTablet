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
    /// Boot.xaml 的交互逻辑
    /// </summary>
    public partial class Boot : Page
    {
        public Boot()
        {
            InitializeComponent();
        }
        DispatcherTimer dtLoadingPage;
        DispatcherTimer dtBIOS;
        int blumed = 0;
        bool Completed = false;
        public bool isPOST =true;
        public bool isInterruped = false;
        public bool isCompleted
        {
            get { return Completed; }
            set { Completed = value; }

        }

        private void BootScreen_Loaded(object sender, RoutedEventArgs e)
        {
            dtLoadingPage = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(2700) }; dtLoadingPage.Tick += DtLoadingPage_Tick;
            dtBIOS = new DispatcherTimer() {Interval = TimeSpan.FromMilliseconds(150) }; dtBIOS.Tick += DtBIOS_Tick;
            dtLoadingPage.Start();
            dtBIOS.Start();
         //todo blume animation
            
        }

        private void DtBIOS_Tick(object sender, EventArgs e)
        {
            blumed++;
            ColorAnimation caColorAni = new ColorAnimation() { 
                From = Color.FromRgb(0, 0, 0),
                To = Color.FromRgb(255, 255, 255), 
                Duration = TimeSpan.FromMilliseconds(300), EasingFunction = new PowerEase() {Power = 2, EasingMode = EasingMode.EaseIn } };
            Path thepath = (Path)FindName("blum" + blumed.ToString());
            thepath.Fill.BeginAnimation(SolidColorBrush.ColorProperty, caColorAni);
            if(blumed==9)
            {
                dtBIOS.Stop();
                dtBIOS.IsEnabled=false;

            }
        }

        private void DtLoadingPage_Tick(object sender, EventArgs e)
        {
            if(isInterruped)
            {
                dtLoadingPage.Stop();
                return;
            }
            
            if (BLUMELOGO.Visibility == Visibility.Visible)
            { 
                BLUMELOGO.Visibility = Visibility.Hidden;
                lblDescription.Visibility = Visibility.Hidden;
                lblCopyright.Visibility = Visibility.Hidden;
               
                dtLoadingPage.Interval = TimeSpan.FromMilliseconds(700);
                return;
            }
             isPOST = false;
            dtLoadingPage.Stop();
            ctOS_LOGO.Visibility = Visibility.Visible;
            progBoot.Visibility = Visibility.Visible;
            DoubleAnimation daProg = new DoubleAnimation() {From = 0,To=1000,Duration= TimeSpan.FromSeconds(3), EasingFunction = new CubicEase() {EasingMode = EasingMode.EaseIn }};//cubic
            daProg.Completed += DaProg_Completed;
            
            progBoot.BeginAnimation(ProgressBar.ValueProperty, daProg);
            
        }

        private void DaProg_Completed(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            ctOS_LOGO.Visibility = Visibility.Hidden;
            progBoot.Visibility = Visibility.Hidden;
            (Application.Current as App).isBootCompleted = true;
        }

        public void InterruptPOST(int state)
        {
            /* 0x0  BIOS
             * 0x1  ::TODO
             */


            isInterruped = true;
            lblDescription.Content = "Loading Setup Utility...";

        }
    }
}
