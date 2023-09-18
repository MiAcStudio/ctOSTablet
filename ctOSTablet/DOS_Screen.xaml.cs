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
using System.Threading;

namespace ctOSTablet
{
    /// <summary>
    /// DOS_Screen.xaml 的交互逻辑
    /// </summary>
    public partial class DOS_Screen : Page
    {
        public DOS_Screen()
        {
            InitializeComponent();
            string s = @"
______ _      _   _________ _____
| ___ \ |    | | | | .  . || ____/
| | / / |    | | | | |\/| || |__
| ___ \ |    | | | | |  | ||  __|
| | / / |____| |_| | |  | || |____
\____/\______/\___/\_|  |_/\______/

BLUMEBIOS(C) 2013 Blume Corporation.
CPU: NUDLE(R) BIOTECHNIC(R) QuadCore CPU B8200G @ 7.20GHz
Speed: 5.34GHz  Count: 4
GPU: TIDIS(C) TG750 7900MHz
HDD0: TIDIS(C) TISTORAGE(R) SSD 120G 

[WAIT]
Initializing USB Controllers .. Done.

Loading Setup ..

";
            AppendText(s);
        }

        public  void AppendText(string txt)
        {
            Task.Run(() => { 

                foreach (string s in txt.Split('\n'))
                {
                    if (s == "[WAIT]\r")
                    {
                        Task.Delay(1000);
                    }
                    else
                    {
                        txtDos.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            txtDos.Text += s + "\n";
                        }));
                    }

                    Task.Delay(200);

                }
            });

        }
    }
}
