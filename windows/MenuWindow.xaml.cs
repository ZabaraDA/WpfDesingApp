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
using System.Windows.Shapes;
using WpfDesingApp.windows;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace WpfDesingApp.windows
{  
    public partial class MenuWindow : Window
    {
        //DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DoubleAnimation doubleAnimation = new DoubleAnimation();
        bool panelState = true;
        public MenuWindow()
        {
            InitializeComponent();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if(WindowState == WindowState.Normal)
            //{
            //    this.DragMove();
            //}
            //else if(WindowState == WindowState.Maximized)
            //{
            //    WindowState = WindowState.Normal;  
            //}
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ControlPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (panelState == true)
            {
                doubleAnimation.From = ControlPanel.ActualWidth;
                doubleAnimation.To = 90;
                doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
                doubleAnimation.EasingFunction = new QuadraticEase();
                ControlPanel.BeginAnimation(WidthProperty, doubleAnimation);
                ControlPanelImage.Source = new BitmapImage( new Uri("/images/ExpandImage.png", UriKind.Relative));
                panelState = false;
            }
            else if (panelState == false)
            {
                doubleAnimation.From = ControlPanel.ActualWidth;
                doubleAnimation.To = 300;
                doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
                doubleAnimation.EasingFunction = new QuadraticEase();
                ControlPanel.BeginAnimation(WidthProperty, doubleAnimation);
                ControlPanelImage.Source = new BitmapImage(new Uri("/images/ReduceImage.png", UriKind.Relative));
                panelState =true;
            }


        }
    }
}
