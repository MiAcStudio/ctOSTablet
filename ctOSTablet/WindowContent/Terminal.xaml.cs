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
using static System.Net.Mime.MediaTypeNames;

namespace ctOSTablet
{
    /// <summary>
    /// Terminal.xaml 的交互逻辑
    /// </summary>
    public partial class Terminal : ctOSWindowContentBase
    {
        public Terminal()
        {
            InitializeComponent();
            this.WindowTitle = "Terminal";
            _prompt = "C:\\> ";

            DispatcherTimer dtCursor = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };
            dtCursor.Tick += DtCursor_Tick;

            dtCursor.Start();
        }

        private void DtCursor_Tick(object sender, EventArgs e)
        {

        }

        string _prompt;

        private void rTerminal_KeyDown(object sender, KeyEventArgs e)
        {

            
        }

        private void rTerminal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                string command = ((rTerminal.Document.Blocks.Last() as Paragraph).
                    Inlines.FirstInline as Run).Text.Remove(0, _prompt.Length);
                if (command == "")
                    rTerminal.Document.Blocks.Add(new Paragraph(new Run(_prompt)));
                else
                {
                    rTerminal.Document.Blocks.Add(new Paragraph(new Run(string.Format("Running command \"{0}\".", command))));
                    rTerminal.Document.Blocks.Add(new Paragraph(new Run(_prompt)));
                }

            }
            if (e.Key == Key.Back)
            {
                if (((rTerminal.Document.Blocks.Last() as Paragraph).
                    Inlines.FirstInline as Run).Text != _prompt)
                    ((rTerminal.Document.Blocks.Last() as Paragraph).
        Inlines.FirstInline as Run).Text = ((rTerminal.Document.Blocks.Last() as Paragraph).
        Inlines.FirstInline as Run).Text.Remove(((rTerminal.Document.Blocks.Last() as Paragraph).
        Inlines.FirstInline as Run).Text.Length - 1, 1);
            }

            if(e.Key == Key.Space)
                ((rTerminal.Document.Blocks.Last() as Paragraph).
    Inlines.FirstInline as Run).Text += " ";
            /*
            if ((int)e.Key >= 34 && (int)e.Key <= 43)
                ((rTerminal.Document.Blocks.Last() as Paragraph).
                    Inlines.FirstInline as Run).Text += e.Key.ToString().Replace("D", "");
            if ((int)e.Key >= 44 && (int)e.Key <= 69)
                ((rTerminal.Document.Blocks.Last() as Paragraph).
                    Inlines.FirstInline as Run).Text +=
                    (Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled ?
                    e.Key.ToString() : e.Key.ToString().ToLower();*/
        }

        private void rTerminal_TextInput(object sender, TextCompositionEventArgs e)
        {
            ((rTerminal.Document.Blocks.Last() as Paragraph).
    Inlines.FirstInline as Run).Text += e.Text;
        }
    }
}
