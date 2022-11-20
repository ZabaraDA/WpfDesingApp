using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using WpfDesingApp.databases;

namespace WpfDesingApp.pages
{
    /// <summary>
    /// Логика взаимодействия для DataUserPage.xaml
    /// </summary>
    public partial class DataUserPage : Page
    {
        TestDatabasesEntities databases = new TestDatabasesEntities();

        public DataUserPage()
        {
            InitializeComponent();

            var itemsProfessionList = databases.Должность.ToList();
            itemsProfessionList.Insert(0, new Должность
            {
                Наименование = "Все должности"
            });
            ProfessionSearchComboBox.ItemsSource = itemsProfessionList.ToList();
            ProfessionSearchComboBox.DisplayMemberPath = "Наименование";
            ProfessionSearchComboBox.SelectedIndex = 0;

            FamilyStatusSearchComboBox.SelectedIndex = 0;
            MilitaryServiceSearchComboBox.SelectedIndex = 0;
            GenderSearchComboBox.SelectedIndex = 0;
            AccountSearchComboBox.SelectedIndex = 0;
            SearchUserDataUpdate();

        }

        private void SearchUserDataUpdate()
        {
            var itemUsers = databases.Сотрудник.ToList();
            int numberOfUsers = databases.Сотрудник.Count();
            NumberOfUsersTextBlock.Text = numberOfUsers.ToString();

            if (NameSearchTextBox.Text != "" && NameSearchTextBox.Text != null)
            {
                itemUsers = itemUsers.Where(x => x.Фамилия.ToLower().Contains(NameSearchTextBox.Text.ToLower()) || x.Имя.ToLower().Contains(NameSearchTextBox.Text.ToLower()) || x.Отчество.ToLower().Contains(NameSearchTextBox.Text.ToLower())).ToList();
            }
            if (FamilyStatusSearchComboBox.SelectedIndex > 0)
            {
                if (FamilyStatusSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Where(x => x.СемейноеПоложение.Equals(true)).ToList();
                }
                else if (FamilyStatusSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = itemUsers.Where(x => x.СемейноеПоложение.Equals(false)).ToList();
                }
            }
            if (ProfessionSearchComboBox.SelectedIndex > 0)
            {
                itemUsers = itemUsers.Where(x => x.КодДолжности.Equals(ProfessionSearchComboBox.SelectedIndex)).ToList();
            }
            if (GenderSearchComboBox.SelectedIndex > 0)
            {
                if (GenderSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Where(x => x.Пол.Equals(true)).ToList();
                }
                else if (GenderSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = itemUsers.Where(x => x.Пол.Equals(false)).ToList();
                }
            }
            if (MilitaryServiceSearchComboBox.SelectedIndex > 0)
            {
                if (MilitaryServiceSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Where(x => x.ВоеннаяСлужба.Equals(true)).ToList();
                }
                else if (MilitaryServiceSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = itemUsers.Where(x => x.ВоеннаяСлужба.Equals(false)).ToList();
                }
            }
            if (AccountSearchComboBox.SelectedIndex > 0)
            {
                int[] idUsers = databases.УчётнаяЗапись.Select(x => x.КодСотрудника).ToArray();
                var r = itemUsers.ToList();

                for (int i = 0; i < idUsers.Length; i++)
                {
                    r = r.Where(x => x.Код != idUsers[i]).ToList();
                }
                if (AccountSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Except(r).ToList();
                    
                }
                else if (AccountSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = r.ToList();
                }
            }

            numberOfUsers = itemUsers.Count();
            FilterNumberOfUserTextBlock.Text = numberOfUsers.ToString();
            UserListView.ItemsSource = itemUsers.ToList();
        }
        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchUserDataUpdate();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchUserDataUpdate();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage());
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userItem = UserListView.SelectedItem as Сотрудник;
            if (userItem != null)
            {
                var lp = databases.УчётнаяЗапись.Where(x => x.КодСотрудника.Equals(userItem.Код)).FirstOrDefault();
                if (lp == null)
                {
                    databases.Сотрудник.Remove(userItem);
                    databases.SaveChanges();
                    SearchUserDataUpdate();
                }
                else
                {
                    MessageBox.Show("У сотрудника есть связанный аккаунт");
                }
            }
           

        }

        private void ChangeParametersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ProfessionSearchComboBox.SelectedIndex = 0;
            NameSearchTextBox.Text = "";
            FamilyStatusSearchComboBox.SelectedIndex = 0;
            MilitaryServiceSearchComboBox.SelectedIndex = 0;
            GenderSearchComboBox.SelectedIndex = 0;
            AccountSearchComboBox.SelectedIndex = 0;
        }
    }
}
