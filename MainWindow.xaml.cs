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
using WpfDesingApp.csclasses;
using WpfDesingApp.databases;
using WpfDesingApp.windows;

namespace WpfDesingApp
{

    public partial class MainWindow : Window
    {
        TestDatabasesEntities testDatabases = new TestDatabasesEntities();
        DispatcherTimer dispatcherTimer = new DispatcherTimer(); // Для блокировки входа на определённое время нужно обратится к таймеру
        bool incorrectly = false; // Логическая переменная, которая определяет был ли допущен неверный ввод логина или пароля
        int timerTick = 9; // Время блокировки после неверного ввода 
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1); // Определяет интервал выполнения для таймера ( в данный момент 1 секунда)
            dispatcherTimer.Tick += DispatcherTimer_Tick; // Каждая иттерация таймера после завершения интервала в 1 секунду обращается
                                                          // к методу DispatcherTimer_Tick
            TimerLabel.Content = timerTick.ToString();

            if (StaticDataClass.timerStart == true)
            {
                OpenButton.IsEnabled = false; // Выключить кнопку 
                AuthorizationStackPanel.Visibility = Visibility.Hidden; // Скрыть StackPanel, которая включает в себя LoginBox и PasswordBox
                TimerStackPanel.Visibility = Visibility.Visible; // Сделать видимой StackPanel, которая включает в себя таймер
                                                                 // Теперь нельзя ввести логин и пароль
                dispatcherTimer.Start(); // Запускает таймер
            }
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

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // Найти в БД строку с совпадающими логином и паролем
            var lp = testDatabases.УчётнаяЗапись.Where(x => x.Логин.Equals(LoginTextBox.Text) && x.Пароль.Equals(PasswordBox.Password)).FirstOrDefault();

            if (lp == null) // Если совпадений нет, то FirstORDefault вернёт null
            {
                LoginTextBox.Text = ""; // Очистить поле ввода логина
                PasswordBox.Password = ""; // Очистить поле ввода пароля

                if (incorrectly == false) // Если ошибка ввода логина или пароля допущена 1 раз
                {
                    MessageBox.Show("Логин или пароль введены неверно: \n\n1) Проверьте правильность ввода\n\n2) Обратитесь к администратору", "Ошибка авторизации");
                    incorrectly = true; // Теперь вход только с каптчей
                }
                else if (incorrectly == true) //  Если ошибка ввода логина или пароля допущена более 1 раза
                {
                    AuthorizationStackPanel.Visibility = Visibility.Hidden; // Скрыть StackPanel, которая включает в себя LoginBox и PasswordBox
                    TimerStackPanel.Visibility = Visibility.Visible; // Сделать видимой StackPanel, которая включает в себя таймер
                    // Теперь нельзя ввести логин и пароль
                    OpenButton.IsEnabled = false;
                    dispatcherTimer.Start(); // Запускает таймер

                }

            }
            else if (incorrectly == true) // Если ошибка ввода логина или пароля допущена более 1 раза, но логин и пароль введены верно
            {

                StaticDataClass.id = lp.КодСотрудника; // Присвоить переменной значение id равное UserID из найденной строки БД 
                CreateCaptchaWindow createCaptchaWindow = new CreateCaptchaWindow();
               
                createCaptchaWindow.ShowDialog(); // Открыть окно с каптчей
                // Метод ShowDialog() заблокирует возможность взаимодействия c окном авторизации пока открыто окно с каптчей
                this.Close();
            }
            else if (incorrectly == false) // Если логин и пароль были верно введены с первого раза
            {
                StaticDataClass.id = lp.КодСотрудника; // Присвоить переменной значение id равное UserID из найденной строки БД 
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show(); // Открыть главное меню
                this.Close(); // И закрыть текущее окно
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length > 0)
            {
                border2.Visibility = Visibility.Visible;
            }
            else
            {
                border2.Visibility = Visibility.Collapsed;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e) // Метод содержит все операции, которые происходят при каждой иттерации таймера
        {
            TimerLabel.Content = timerTick.ToString(); // Вывести на экран текущее время до отключения блокировки
            timerTick--; // При каждой иттерации уменьшает значение на 1
            if (timerTick == 0) // Если 10 секунд ожидания закончились
            {
                StaticDataClass.timerStart = false;
                dispatcherTimer.Stop(); // Остановить таймер
                timerTick = 9; // Вернуть изначальное значение переменной
                TimerStackPanel.Visibility = Visibility.Hidden; // Скрыть StackPanel, которая включает в себя таймер
                AuthorizationStackPanel.Visibility = Visibility.Visible; // Сделать видимой StackPanel, которая включает в себя LoginBox и PasswordBox
                                                                         // Теперь снова можно ввести логин и пароль
                TimerLabel.Content = timerTick.ToString();
                OpenButton.IsEnabled = true;
            }
        }


    }
}
