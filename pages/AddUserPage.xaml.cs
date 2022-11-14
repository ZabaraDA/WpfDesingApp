using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
using WpfDesingApp.databases;

namespace WpfDesingApp.pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        TestDatabasesEntities testDatabases = new TestDatabasesEntities();
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Multiselect = false,
            Filter = "Images (*.JPG; *.PNG)| *.JPG;*.PNG"
        };

        bool addWithAccount;
        bool correctly;

        byte[] photoProfile;

        string exeptionData;
        public AddUserPage()
        {
            InitializeComponent();

            var professionItems = testDatabases.Должность.ToList();
            professionItems.Insert(0, new Должность
            {
                  Наименование = "Не выбрано"
            });
            ProfessionComboBox.ItemsSource = professionItems.ToList();
            ProfessionComboBox.DisplayMemberPath = "Наименование";
            ProfessionComboBox.SelectedIndex = 0;

            GenderComboBox.SelectedIndex = 0;
            MilitaryServiceComboBox.SelectedIndex = 0;
            FamilyStatusComboBox.SelectedIndex = 0;
            AccessComboBox.SelectedIndex = 0;   
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                PhotoProfile.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                photoProfile = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text == "" || NameTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите имя";
            }
            else if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите имя";
            }
            else if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите имя";
            }


            if (correctly == true)
            {
                testDatabases.Сотрудник.Add(new Сотрудник
                {
                    Код = testDatabases.Сотрудник.Count(),
                    Имя = NameTextBox.Text,
                    Фамилия = SurnameTextBox.Text,
                    Отчество = PatronymicTextBox.Text,
                    АдресПроживание = AddressTextBox.Text,
                    ДатаРождения = Convert.ToDateTime(DOBDatePicker.Text),
                    ДатаТрудоустройства = Convert.ToDateTime(EmploymentDatePicker.Text),
                    КодДолжности = ProfessionComboBox.SelectedIndex,
                    ИНН = INNTextBox.Text,
                    ОМС = OMSTextBox.Text,
                    СНИЛС = SNILSTextBox.Text,
                    Почта = EmailTextBox.Text,
                    Телефон = PhoneTextBox.Text,
                    Фото = photoProfile,
                    ВоеннаяСлужба = Convert.ToBoolean(MilitaryServiceComboBox.SelectedIndex - 1),
                    Пол = Convert.ToBoolean(GenderComboBox.SelectedIndex),
                    СемейноеПоложение = Convert.ToBoolean(FamilyStatusComboBox.SelectedIndex),
                });
            }
            if (addWithAccount == true)
            {
                testDatabases.УчётнаяЗапись.Add(new УчётнаяЗапись
                {
                    Код = testDatabases.УчётнаяЗапись.Count(),
                    Логин = LoginTextBox.Text,
                    Пароль = PasswordBox.Password,
                    Доступ = AccessComboBox.SelectedIndex,
                    КодСотрудника = testDatabases.Сотрудник.Count()

                });
            }

            if (correctly == true)
            {
                testDatabases.SaveChanges();
            }
            else
            {
                correctly = true;
            }
           
        }

        private void ClearInputFields_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (addWithAccount == false)
            {
                addWithAccount = true;
                AccountDataStackPanel.Visibility = Visibility.Visible;
                AddAccountButton.Content = "Удалить аккаунт";
                AccountTextBlock.Visibility = Visibility.Collapsed;
            }
            else if (addWithAccount == true)
            {
                addWithAccount = false;
                AccountDataStackPanel.Visibility= Visibility.Collapsed;
                AddAccountButton.Content = "Добавить аккаунт";
                AccountTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
