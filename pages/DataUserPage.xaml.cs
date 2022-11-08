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
            //int[] idUsers = databases.Аккаунт.Select(x => x.КодСотрудника).ToArray();
            //List<Сотрудник> r = databases.Сотрудник.ToList(); ;
            //var s = databases.Сотрудник.ToList();
            //for (int i = 0; i < idUsers.Length; i++)
            //{
            //    r = r.Where(x => x.Код != idUsers[i]).ToList();
            //}
            //UserListView.ItemsSource = r.ToList();
            SearchUserDataUpdate();
            
        }

        private void SearchUserDataUpdate()
        {
            var itemUser = databases.Сотрудник.ToList();
            int numberOfProducts = databases.Сотрудник.Count();
            // вывод количества

        }
        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            SearchUserDataUpdate(); 
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) 
        {                                                                                
            SearchUserDataUpdate(); 
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeParametersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
