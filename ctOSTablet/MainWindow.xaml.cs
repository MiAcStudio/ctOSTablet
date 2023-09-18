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
using System.Windows.Threading;

namespace ctOSTablet
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dtHelper;
        Security pageSec;
        Boot pageBoot;
        DOS_Screen pageDos;

        public MainWindow()
        {
            (Application.Current as App).sysEvent = new SystemEvent();
            (Application.Current as App).sysEvent.SystemShutdown += SysEvent_SystemShutdown;
            (Application.Current as App).sysEvent.LoginSuccessful += SysEvent_LoginSuccessful;
            InitializeComponent();
            pageDos = new DOS_Screen();
            pageBoot = new Boot();
            pageSec = new Security();
        }


        private async void SysEvent_LoginSuccessful()
        {
            await Task.Delay(800);
            Desktop desktop = new Desktop();
            frame.Navigate(desktop);
        }

        private async void SysEvent_SystemShutdown()
        {
            frame.Content = null;
            await Task.Delay(500);
            Application.Current.Shutdown();
        }

        private void DtHelper_Tick(object sender, EventArgs e)
        {
            if ((Application.Current as App).isBootCompleted)
            {
                frame.Source = null;
                System.Threading.Thread.Sleep(1000);
                frame.NavigationService.Navigate(pageSec);
                (Application.Current as App).isBootCompleted = false;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Maximized;
            
            await Task.Delay(2000);
            frame.NavigationService.Navigate(pageBoot);
            dtHelper = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(400) };
            dtHelper.Tick += DtHelper_Tick;
            dtHelper.Start();
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(pageBoot.isPOST && pageBoot.isInterruped == false && e.Key== Key.Delete)
            {
                pageBoot.InterruptPOST(0);

                await Task.Delay(3000);
                frame.Content = null;
                await Task.Delay(500);
                frame.NavigationService.Navigate(pageDos);
                await Task.Delay(3000);

            }
        }
    }
}
