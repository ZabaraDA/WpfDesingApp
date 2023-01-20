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
using WpfDesingApp.csclasses;
using WpfDesingApp.pages;
using WpfCustomControlLibrary;
using WpfDesingApp.Properties;
using System.Windows.Markup;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;

namespace WpfDesingApp.windows
{
    public partial class MenuWindow : Window
    {
        DoubleAnimation doubleAnimation = new DoubleAnimation();

        int quantity = 0;
        readonly List<(Page,string,string)> navigationButtonList = new List<(Page, string, string)>()
        {
          (new ProfilePage(),"Профиль","SettingsPathData"),
          (new AddUserPage(), "Аккаунты","ProfilePathData"),
          (new DataUserPage(), "Сотрудники","ProfilePathData"),
        };
        public MenuWindow()
        {

            InitializeComponent();

            ExitPath.Data = Geometry.Parse(PathDataClass.exitData);
            WindowStatePath.Data = Geometry.Parse(PathDataClass.fullScreenData);
            HidePath.Data = Geometry.Parse(PathDataClass.hideData);
            StatusBarLabel.Content = "Главное меню - Приветствие";

            MenuFrame.Navigate(new WelcomePage());

            ButtonGenerator();



        }

        private void ButtonGenerator()
        {
            for (int i = 0; i < navigationButtonList.Count; i++)
            {
                NavigationButton navigationButton = new NavigationButton()
                {
                    Content = navigationButtonList[i].Item2,
                    Data = (Geometry)Application.Current.FindResource(navigationButtonList[i].Item3),
                    Tag = quantity++,
                };

                navigationButton.Click += Button_Click;
                ControlStackPanel.Children.Add(navigationButton);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = sender as NavigationButton;

            MenuFrame.Navigate(navigationButtonList[(int)a.Tag].Item1); 
            
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
                WindowStatePath.Data = Geometry.Parse(PathDataClass.collapseData);
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStatePath.Data = Geometry.Parse(PathDataClass.fullScreenData);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Настройки приложения";
            MenuFrame.Navigate(new SettingsPage());
        }

        private void AddProductsButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Добавление товара";
            MenuFrame.Navigate(new AddProductPage());
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Просмотр товаров";
            MenuFrame.Navigate(new DataProductPage());
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Добавление новых пользователей";
            MenuFrame.Navigate(new AddUserPage());
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Просмотр данных пользователей";
            MenuFrame.Navigate(new DataUserPage());
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBarLabel.Content = "Главное меню - Просмотр профиля";
            MenuFrame.Navigate(new ProfilePage());
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {
            StaticDataClass.timerStart = false;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }  
        private void ControlPanel_MouseEnter(object sender, MouseEventArgs e)
        {
             
            doubleAnimation.From = ControlGrid.ActualWidth;
            doubleAnimation.To = 210;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, doubleAnimation);
           
            //panelState = true;
        }

        private void ControlPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            doubleAnimation.From = ControlGrid.ActualWidth;
            doubleAnimation.To = 70;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, doubleAnimation);
          
            //panelState = false;
        }
    }
}
