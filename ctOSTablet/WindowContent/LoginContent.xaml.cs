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
    /// LoginContent.xaml 的交互逻辑
    /// </summary>
    public partial class LoginContent : ctOSWindowContentBase
    {
        bool _isLoggedIn;
        public LoginContent()
        {
            InitializeComponent();
            _isLoggedIn = false;
            WindowClosed += LoginContent_WindowClosed;
        }

        private void LoginContent_WindowClosed(object sender,EventArgs e)
        {
            (Application.Current as App).sysEvent.Trigger_SystemShutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtAccount.Text == "BlumeInc" && txtPassword.Password == "admin")
            {
                _isLoggedIn=true;

               
                (Application.Current as App).sysEvent.Trigger_LoginSuccessful();

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).sysEvent.Trigger_SystemShutdown();
           //MessageBox.Show( VisualTreeHelper.GetParent(this).GetType().ToString());
        }
    }
}
