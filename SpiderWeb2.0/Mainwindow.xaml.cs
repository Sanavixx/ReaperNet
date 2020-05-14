// Author: Edward Champa  
// Updated: 12 May 2020

using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SpiderWeb2._0
{
    public partial class Mainwindow : System.Windows.Window
    {
        public Mainwindow()
        {
            InitializeComponent();
            Home_Page.Visibility = Visibility.Collapsed;
            nav_panel.Visibility = Visibility.Collapsed;
            MouseDown += Window_MouseDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        //Sets Tolltip Visibility For Navigation Panel
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_comp.Visibility = Visibility.Collapsed;
                tt_softwarte.Visibility = Visibility.Collapsed;
                tt_ad.Visibility = Visibility.Collapsed;
                tt_tools.Visibility = Visibility.Collapsed;
                tt_scripts.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_comp.Visibility = Visibility.Visible;
                tt_softwarte.Visibility = Visibility.Visible;
                tt_ad.Visibility = Visibility.Visible;
                tt_tools.Visibility = Visibility.Visible;
                tt_scripts.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
            }

        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.2;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Release_Btn_Click(object sender, RoutedEventArgs e)
        {
            release_notes_window.Visibility = Visibility.Visible;
        }

        private void ReleaseClose_Btn_Click(object sender, RoutedEventArgs e)
        {
            Release_Btn.IsEnabled = true;
        }

        private async void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            Home_Page.Visibility = Visibility.Visible;
            nav_panel.Visibility = Visibility.Visible;
            await Task.Delay(200);
            openingBG.Visibility = Visibility.Collapsed;
        }

        private void Release_Btn_Click_1(object sender, RoutedEventArgs e)
        {
            Release_Btn.IsEnabled = false;
        }

        private void SD_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window SDWin = new Window();
            SDWin.Owner = this;
            if (SDWin.ShowDialog() == true) {}
        }

        private void HomeBtn1_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }

        private void HomeBtn2_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }
    }

    static class DesignModeTool
    {
        public static readonly DependencyProperty IsHiddenProperty =
            DependencyProperty.RegisterAttached("IsHidden",
                typeof(bool),
                typeof(DesignModeTool),
                new FrameworkPropertyMetadata(false,
                    new PropertyChangedCallback(OnIsHiddenChanged)));

        public static void SetIsHidden(FrameworkElement element, bool value)
        {
            element.SetValue(IsHiddenProperty, value);
        }

        public static bool GetIsHidden(FrameworkElement element)
        {
            return (bool)element.GetValue(IsHiddenProperty);
        }

        private static void OnIsHiddenChanged(DependencyObject d,
                                              DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d)) return;
            var element = (FrameworkElement)d;
            element.RenderTransform = (bool)e.NewValue
               ? new ScaleTransform(0, 0)
               : null;
        }
    }

}
