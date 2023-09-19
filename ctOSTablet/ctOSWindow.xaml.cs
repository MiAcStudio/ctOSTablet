using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ctOSTablet
{
    /// <summary>
    /// Interaction.xaml 的交互逻辑
    /// </summary>
    public partial class ctOSWindow : UserControl
    {

        public void SetWndContent(ctOSWindowContentBase e)
        {
            this.Width = e.Width; this.Height= e.Height+20;
            HeaderTitle.Text = e.WindowTitle;

            gContent.Content=e;
        }
        public ctOSWindow()
        {
            InitializeComponent();
        }
        public ctOSWindow(ctOSWindowContentBase e)
        {
            InitializeComponent();
            this.Width = e.Width; this.Height = e.Height + 20;
            HeaderTitle.Text = e.WindowTitle;
            gContent.Content = e;
        }
        private void vbClose_Click(object sender, RoutedEventArgs e)
        {
            if(gContent.Content != null)
            {
                (gContent.Content as ctOSWindowContentBase).RaiseWindowClosed();
            }
            this.Visibility = Visibility.Hidden;
            ((Grid)this.Parent).Children.Remove(this);
        }
        private void vbClose_MouseEnter(object sender, MouseEventArgs e)
        {
            vbClose.Background = Brushes.DimGray;
            //ExitIcon.Fill = Brushes.Black;
        }

        private void vbClose_MouseLeave(object sender, MouseEventArgs e)
        {
            vbClose.Background = Brushes.Black;
            //ExitIcon.Fill = Brushes.White;
        }

        bool _isDragMoving = false;
        Point _relativePoint;
        Thickness _formerMargin;
        double _deltaX=0;
        double _deltaY=0;
        private void gHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var container = VisualTreeHelper.GetParent(this) as Grid;
            _relativePoint = e.GetPosition(container);
            if(_formerMargin == null)
                _formerMargin = this.Margin;
            gHeader.CaptureMouse();
            _isDragMoving = true;

            if (container != null)
            {
                IEnumerable<UIElement> uiE = container.Children.OfType<UIElement>().Where(x => x != this);//枚举类型定义
                if (uiE.Count() > 0)//判断 除去用户选择的控件，是否还有其他控件。
                {
                    var maxZ = uiE.Select(x => Grid.GetZIndex(x)).Max();
                    Grid.SetZIndex(this, maxZ + 1);//置于最顶层
                }
            }
        }

        private void gHeader_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragMoving = false;
           // _formerMargin = this.Margin;
            gHeader.ReleaseMouseCapture();
            var container = VisualTreeHelper.GetParent(this) as UIElement;
            Point current = e.GetPosition(container);

            _deltaX += current.X - _relativePoint.X;
            _deltaY += current.Y - _relativePoint.Y;
        }

        private void gHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragMoving)
            {
                var container = VisualTreeHelper.GetParent(this) as UIElement;
                Point current = e.GetPosition(container);
                //Vector v = Point.Subtract(current, _relativePoint);
                //this.Margin = new Thickness(_formerMargin.Left + v.X, _formerMargin.Top + v.Y, _formerMargin.Left - v.X, _formerMargin.Top - v.Y);
                this.RenderTransform = new TranslateTransform(_deltaX+ current.X - _relativePoint.X,_deltaY+ current.Y - _relativePoint.Y);

            }
        }

        private void gContent_GotFocus(object sender, RoutedEventArgs e)
        {
            var container = VisualTreeHelper.GetParent(this) as Grid;
            if (container != null)
            {
                IEnumerable<UIElement> uiE = container.Children.OfType<UIElement>().Where(x => x != this);//枚举类型定义
                if (uiE.Count() > 0)//判断 除去用户选择的控件，是否还有其他控件。
                {
                    var maxZ = uiE.Select(x => Grid.GetZIndex(x)).Max();
                    Grid.SetZIndex(this, maxZ + 1);//置于最顶层
                }
            }
        }
    }
}
