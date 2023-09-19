using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ctOSTablet
{
    public class ctOSWindowContentBase : UserControl
    {
       // public delegate void WindowClosedEventHandler(object sender, EventArgs e);
        public event EventHandler WindowClosed;
        public string WindowTitle = "";
        public bool IsWindowSizable = true;
        protected virtual void OnWindowClosed(EventArgs e)
        {
            EventHandler handler = WindowClosed;
            if(handler!=null)
            handler.Invoke(this, e);
        }
        public void RaiseWindowClosed()
        {
            OnWindowClosed(EventArgs.Empty);
        }

    }
}
