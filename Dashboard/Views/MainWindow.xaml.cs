using Dashboard.Services;
using Dashboard.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Dashboard.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
            DataContext = new MainViewModel(new AppSettings(), new DataService(), new PaletteHelper());
            InitializeComponent();
        }


        private double _hOff;
        private double _vOff;
        private Point _scrollMousePoint;

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer))
                return;

            scrollViewer.ReleaseMouseCapture();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer))
                return;

            _scrollMousePoint = e.GetPosition(scrollViewer);
            _hOff = scrollViewer.HorizontalOffset;
            _vOff = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        private void UIElement_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer))
                return;

            if (scrollViewer.IsMouseCaptured)
            {
                var position = e.GetPosition(scrollViewer);
                var moveToX = _scrollMousePoint.X - position.X;
                var moveToY = _scrollMousePoint.Y - position.Y;

                if (Math.Abs(moveToX) > 1 || Math.Abs(moveToY) > 1)
                {
                    scrollViewer.ScrollToHorizontalOffset(_hOff + moveToX);
                    scrollViewer.ScrollToVerticalOffset(_vOff + moveToY);
                }
            }
        }
    }
}
