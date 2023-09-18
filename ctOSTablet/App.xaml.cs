using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ctOSTablet
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        bool _isBootCompleted = false;
        public bool isBootCompleted
        {
            get { return _isBootCompleted; }
            set { _isBootCompleted = value; }
        }
        public SystemEvent sysEvent;
    }
}
