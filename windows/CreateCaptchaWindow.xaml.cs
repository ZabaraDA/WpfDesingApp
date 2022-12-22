using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfDesingApp.csclasses;

namespace WpfDesingApp.windows
{
    public partial class CreateCaptchaWindow : Window
    {
        Random randomСoordinates = new Random();
        // Рандом для генерации случайных координат
        List<(int x, int y)> coordinatesList = new List<(int x, int y)>();
        // Список координат. Каждый элемент хранит координаты по x и y

        string captchaContentString; // Строка хранит все символы капчи для сравнения с вводом пользователя
        bool correctCoordinatesBool = false; // Проверяет корректность случайной координаты по x или y
                                             // Символы капчи не должны создаваться в одной координате
        string symbolString; // Строка хранит один символ для отрисовки на экране
        int xCoordinates; // Координата по x
        int yCoordinates; // Координата по y
        int distant = 20; // Минимальная допустимая дистанция между символами капчи                  

        public CreateCaptchaWindow()
        {
            InitializeComponent();
            CreateCaptcha();
        }

        
        private void CreateCaptcha() // Метод генерации капчи
        {
            CaptchaGrid.Children.Clear(); // При каждом вызове метода нужно очистить Grid
            coordinatesList.Add((randomСoordinates.Next(350), randomСoordinates.Next(350))); //Случайные значения для первой координаты

            Ellipse ellipse = new Ellipse(); //Создание нового экземпляра Ellipse 
            ellipse.Fill = System.Windows.Media.Brushes.Black; // Заливка черным цветом

            ellipse.Height = 20; 
            ellipse.Width = 20;
            // Высота и ширина эллипса равны, чтобы отрисовать круг

            ellipse.HorizontalAlignment = HorizontalAlignment.Left; 
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            // Отсчёт координат от верхнего левого угла

            ellipse.RenderTransform = new TranslateTransform(coordinatesList[0].x - 10, coordinatesList[0].y - 10);
            CaptchaGrid.Children.Add(ellipse);
            // Добавить круг, чтобы обозначить начало капчи

            for (int i = 0; i < 5; i++) // Значение i - колличество символов в капче
            {
                symbolString = ""; // Строка с символом для отрисовки очищается при каждом проходе цикла
                captchaContentString += symbolString += (char)randomСoordinates.Next(97, 122);// Добавление случайного символа
                // В диапазоне (97, 122) преобразование чисел в английские буквы (в соответствии с ASCII)
                //captchaContentString += symbolString += (char)randomСoordinates.Next(1040, 1104); //Для русских букв

                Line directionLine = new Line(); // Создание новой линии
                // Линии будут соединять символы капчи 
                directionLine.Stroke = System.Windows.Media.Brushes.Black;// Заливка черным цветом

                directionLine.X1 = coordinatesList[i].x; 
                directionLine.Y1 = coordinatesList[i].y;
                // Line имеет координаты начальной точки (X1 и Y1) и конечной точки (X2 и Y2)
                correctCoordinatesBool = false;
                while (correctCoordinatesBool != true) // Проверка координаты x на корректность
                {                                     
                    xCoordinates = randomСoordinates.Next(350); // Координата меняет значение пока не выполнится условие
                    for (int j = 0; j < coordinatesList.Count;)
                    {

                        if (xCoordinates + distant < coordinatesList[j].x || xCoordinates - distant > coordinatesList[j].x)
                        {
                            j++;
                            correctCoordinatesBool = true;
                        }
                        else
                        {
                            correctCoordinatesBool = false;
                            break;
                        }
                    }
                }
                correctCoordinatesBool = false;
                while (correctCoordinatesBool != true) // Проверка координаты x на корректность
                {
                    yCoordinates = randomСoordinates.Next(350); // Координата меняет значение пока не выполнится условие
                    for (int j = 0; j < coordinatesList.Count;)
                    {

                        if (yCoordinates + distant < coordinatesList[j].y || yCoordinates - distant > coordinatesList[j].y)
                        {
                            j++;
                            correctCoordinatesBool = true;
                        }
                        else
                        {
                            correctCoordinatesBool = false;
                            break;
                        }
                    }
                }
                coordinatesList.Add((xCoordinates, yCoordinates)); // Верно подобранные координаты загружаются в список

                directionLine.X2 = coordinatesList[i + 1].x;
                directionLine.Y2 = coordinatesList[i + 1].y;
                // Конечная координата задаётся из списка

                FormattedText formattedText = new FormattedText( //FormattedText нужен для расширенного форматирования текста
                    symbolString, // Источник текста
                    CultureInfo.GetCultureInfo("en-us"), // Региональные параметры языка (en - английский, us - США)
                    FlowDirection.LeftToRight, // отображение текста слева направо
                    new Typeface(new System.Windows.Media.FontFamily(), // Семейство шрифтов 
                    FontStyles.Italic,// Начертание шрифта
                    FontWeights.Bold, // Плотность шрифта
                    FontStretches.Normal), // Обводка шрифта
                    24, Brushes.Black, // Размер шрифта, Цвет шрифта
                    VisualTreeHelper.GetDpi(this).PixelsPerDip); // Определение DPI монитора для правильного отображения

                Geometry symbolGeometry = formattedText.BuildGeometry(new System.Windows.Point(directionLine.X1, directionLine.Y1));
                // Создание геометрии из форматированного текста в определённых координатах

                var symbolPath = new Path(); 
                symbolPath.Stroke = System.Windows.Media.Brushes.Black;
                symbolPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
                symbolPath.Data = symbolGeometry; // Создание символа зи геометрии

                CaptchaGrid.Children.Add(symbolPath);  // Добавить новый символ в Grid
                CaptchaGrid.Children.Add(directionLine); // Добавить новую линию в Grid
            }
            CaptchaGrid.Children.RemoveAt(10); // Удалить последнюю линию
            coordinatesList.Clear(); // Очистить список координат
        }

        private void CaptchaButton_Click(object sender, RoutedEventArgs e) // При нажатии на кнопку Готово
        {
            if (CaptchaTextBox.Text == captchaContentString) // Если текст введённый пользователем соответствует содержимому капчи
            {

                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show(); // Открыть окно главного меню
                this.Close(); // И закрыть текущее окно

            }
            else // Если пользователь решил капчу неверно
            {
                StaticDataClass.timerStart = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show(); // Вернуть пользователя к окну авторизации
                // В окне авторизации будет активирован таймер блокировки на 10 секунд
                this.Close();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Если пользователь не хочет ввести капчу, то он может завершить работу приложения
        }

        private void ChangeCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
        }
    }
}
