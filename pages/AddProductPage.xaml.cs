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
using WpfDesingApp.databases;

namespace WpfDesingApp.pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        TestDatabasesEntities databases= new TestDatabasesEntities();
        //TradeEntities tradeEntities = new TradeEntities();

        public AddProductPage()
        {
            //DataContext = tradeEntities.Product;
            InitializeComponent();
            //ProductListView.ItemsSource = tradeEntities.Product.ToList();
            //ww.ItemsSource = tradeEntities.Product.ToList();
        }
    }
}
