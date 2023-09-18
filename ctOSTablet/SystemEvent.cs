using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOSTablet
{
    public delegate void ctOSShutdownHandler();
    public delegate void LoginSuccessfulHandler();
    public class SystemEvent
    {
        public event ctOSShutdownHandler SystemShutdown;
        public event LoginSuccessfulHandler LoginSuccessful;
        public void Trigger_SystemShutdown()
        {
            SystemShutdown();
        }
        public void Trigger_LoginSuccessful()
        {
            LoginSuccessful();
        }

    }
}
