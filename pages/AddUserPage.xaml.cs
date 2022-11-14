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
        bool correctly = true;

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
            if (NameTextBox.Text == "" || NameTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите имя";
            }
            else if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите фамилию";
            }
            else if (PatronymicTextBox.Text == "" || PatronymicTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите отчество";
            }
            else if (ProfessionComboBox.SelectedIndex == 0)
            {
                correctly = false;
                exeptionData = "Укажите должность";
            }
            else if (AddressTextBox.Text == "" || AddressTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите место жительства";
            }
            else if (GenderComboBox.SelectedIndex == 0)
            {
                correctly = false;
                exeptionData = "Укажите пол";
            }
            else if (DOBDatePicker.Text == "" || DOBDatePicker.Text == null)
            {
                correctly = false;
                exeptionData = "Укажите дату рождения";
            }
            else if (FamilyStatusComboBox.SelectedIndex == 0)
            {
                correctly = false;
                exeptionData = "Укажите семейное положение";
            }
            else if (INNTextBox.Text == "" || INNTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите ИНН";
            }
            else if (OMSTextBox.Text == "" || OMSTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите ОМС";
            }
            else if (SNILSTextBox.Text == "" || SNILSTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите СНИЛС";
            }
            else if (PhoneTextBox.Text == "" || PhoneTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите номер телефона";
            }
            else if (CitizenshipTextBox.Text == "" || CitizenshipTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите гражданство";
            }
            else if (EmploymentDatePicker.Text == "" || EmploymentDatePicker.Text == null)
            {
                correctly = false;
                exeptionData = "Укажите дату трудоустройства";
            }
            else if (MilitaryServiceComboBox.SelectedIndex == 0)
            {
                correctly = false;
                exeptionData = "Укажите статус военного учёта";
            }
            else if (EmailTextBox.Text == "" || EmailTextBox.Text == null)
            {
                correctly = false;
                exeptionData = "Введите электронную почту";
            }
            else if (correctly == true)
            {
                testDatabases.Сотрудник.Add(new Сотрудник
                {
                    
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
                if(LoginTextBox.Text == "" || LoginTextBox.Text == null)
                {
                    correctly = false;
                    exeptionData = "Введите логин";
                }
                else if (PasswordBox.Password == "" || PasswordBox.Password == null)
                {
                    correctly = false;
                    exeptionData = "Введите пароль";
                }
                else if (PasswordBox.Password != DoublePasswordBox.Password)
                {
                    correctly = false;
                    exeptionData = "Пароли не совпадают";
                }
                else if (AccessComboBox.SelectedIndex == 0)
                {
                    correctly = false;
                    exeptionData = "Укажите уровень доступа к системе";
                }
                else if (correctly == true)
                {
                    testDatabases.УчётнаяЗапись.Add(new УчётнаяЗапись
                    {
                        Логин = LoginTextBox.Text,
                        Пароль = PasswordBox.Password,
                        Доступ = AccessComboBox.SelectedIndex,
                        КодСотрудника = testDatabases.Сотрудник.Count()
                    });
                }
            }

            if (correctly == true)
            {
                try
                {
                    testDatabases.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }
            else
            {
                MessageBox.Show(exeptionData);
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
